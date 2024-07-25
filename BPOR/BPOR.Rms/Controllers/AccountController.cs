﻿using BPOR.Domain.Entities;
using BPOR.Rms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.Authentication;
using NIHR.Infrastructure.Authentication.IDG.SCIM;
using System.Security.Cryptography;

namespace BPOR.Rms.Controllers
{
    public class AccountController(ParticipantDbContext dbContext, IDataProtectionProvider dataProtectionProvider, ILogger<AccountController> logger, IHostEnvironment hostEnvironment, LinkGenerator linkGenerator, IUserAccountStore userAccountStore, IOptions<DevelopmentSettings> developmentSettings, ITransactionalEmailService transactionalEmailService) : Controller
    {
        private readonly ITimeLimitedDataProtector _dataProtector = dataProtectionProvider.CreateProtector("CreateAccountEmailLink").ToTimeLimitedDataProtector();
        private readonly DevelopmentSettings _developmentSettings = developmentSettings.Value;

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult NotRegistered()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult SignedOut()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateAccountModel createAccountModel, CancellationToken token = default)
        {
            if (ModelState.IsValid)
            {
                return await CheckYourEmailAsync(createAccountModel, token);
            }
            else
            {
                return View(createAccountModel);
            }
        }
        private async Task<IActionResult> CheckYourEmailAsync(CreateAccountModel createAccountModel, CancellationToken token = default)
        {
            if (ModelState.IsValid)
            {
                createAccountModel.Email = createAccountModel.Email.Trim().ToLowerInvariant();

                await Task.Delay(1000); // TODO: Rate limit email sending to avoid abuse.
                var code = _dataProtector.Protect(createAccountModel.Email, TimeSpan.FromHours(24));

                var link = linkGenerator.GetUriByName(HttpContext, nameof(VerifyEmailAsync), new { code });

                var emailTemplateKey = await HasAccount(createAccountModel.Email, token)
                    ? "email-rms-registration-attempt"
                    : "email-rms-new-registration";

                var emailContent = await transactionalEmailService.SendAsync(createAccountModel.Email, emailTemplateKey, new { recipientEmail = createAccountModel.Email, link }, token);

                if (!hostEnvironment.IsProduction() && _developmentSettings.EnableUnauthenticatedTestFeatures)
                {
                    ViewData["email-to"] = createAccountModel.Email;
                    ViewData["email-subject"] = emailContent.EmailSubject;
                    ViewData["email-body"] = emailContent.EmailBody;
                    ViewData["email-link"] = link;
                }

                return View("CheckYourEmail", createAccountModel);
            }
            else
            {
                return View(nameof(Create), createAccountModel);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[controller]/verifyemail", Name = nameof(VerifyEmailAsync))]
        public async Task<IActionResult> VerifyEmailAsync(string code, CancellationToken token)
        {
            try
            {
                var email = _dataProtector.Unprotect(code, out var expiration);

                if (await HasAccount(email, token))
                {
                    // Sign in
                    var homeUrl = linkGenerator.GetUriByAction(HttpContext, nameof(HomeController.Index), "Home") + "?sign-in";
                    return Redirect(homeUrl);
                }
                else
                {
                    // Start new account journey
                    return RedirectToRoute(nameof(GatherAccountInformation), new { code });
                }
            }
            catch (CryptographicException ex)
            {
                // Code is invalid or has expired.
                logger.LogWarning(ex, ex?.Message);
            }

            this.AddNotification(new NotificationBannerModel
            {
                Title = "Email verification error",
                Heading = "There was a problem with your email verification code.",
                Body = "You can try to resend the email below."
            });

            return RedirectToAction(nameof(Create));
        }

        private async Task<bool> HasAccount(string email, CancellationToken token)
        {
            var hasAccount = await dbContext.User
                                .IgnoreQueryFilters() // check for active and deleted accounts.
                                .AnyAsync(x => x.ContactEmail == email, token);

            hasAccount = hasAccount || await userAccountStore.UserWithEmailExistsAsync(email, token);
            return hasAccount;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[controller]/GatherAccountInformation", Name = nameof(GatherAccountInformation))]
        public IActionResult GatherAccountInformation([FromQuery] string code, CancellationToken token = default)
        {
            try
            {
                var email = _dataProtector.Unprotect(code, out var expiration);
                ViewData["email"] = email;

                return View(new GatherAccountInformationModel { Code = code });
            }
            catch (CryptographicException ex)
            {
                // Code is invalid or has expired.
                logger.LogWarning(ex, ex?.Message);
            }

            this.AddNotification(new NotificationBannerModel
            {
                Title = "Email verification error",
                Heading = "There was a problem with your email verification code.",
                Body = "You can try to resend the email below."
            });

            return RedirectToAction(nameof(Create));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GatherAccountInformationAsync(GatherAccountInformationModel model, CancellationToken token = default)
        {
            try
            {
                var email = _dataProtector.Unprotect(model.Code, out var expiration);
                ViewData["email"] = email;

                if (ModelState.IsValid)
                {
                    if (await HasAccount(email, token))
                    {
                        this.AddNotification(new NotificationBannerModel
                        {
                            Title = "Account already exists",
                            Heading = "An account already exists for your email address",
                            Body = "Sign in from the RMS homepage"
                        });

                        return View(model);
                    }

                    var userId = await userAccountStore.CreateNewUserAsync(email, model.FirstName, model.LastName, model.Password, token);

                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        return View("New");
                    }
                    else
                    {
                        this.AddNotification(new NotificationBannerModel
                        {
                            Title = "Unable to create user account",
                            Heading = "Unable to create user account",
                        });
                    }
                }
            }
            catch (CryptographicException ex)
            {
                // Code is invalid or has expired.
                logger.LogWarning(ex, ex?.Message);

                this.AddNotification(new NotificationBannerModel
                {
                    Title = "Email verification error",
                    Heading = "There was a problem with your email verification code.",
                    Body = "You can try to resend the email below."
                });

                return RedirectToAction(nameof(Create));
            }
            catch (PasswordPolicyException ex)
            {
                ModelState.AddModelError(nameof(GatherAccountInformationModel.Password), ex.Message);
            }

            return View(model);
        }
    }
}
