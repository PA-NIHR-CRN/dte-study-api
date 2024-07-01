using BPOR.Domain.Entities;
using BPOR.Domain.Entities.Configuration;
using BPOR.Rms.Models;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Authentication.IDG.SCIM;
using NIHR.Infrastructure.Interfaces;
using System.Security.Cryptography;

namespace BPOR.Rms.Controllers
{
    public class AccountController : Controller
    {
        private ITimeLimitedDataProtector _dataProtector;
        private readonly ParticipantDbContext _dbContext;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IContentProvider _contentProvider;
        private readonly LinkGenerator _linkGenerator;
        private readonly IUserAccountStore _userAccountStore;

        public AccountController(ParticipantDbContext dbContext, IDataProtectionProvider dataProtectionProvider, ILogger<AccountController> logger, IEmailService emailService, IHostEnvironment hostEnvironment, IContentProvider contentProvider, LinkGenerator linkGenerator, IUserAccountStore userAccountStore)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("CreateAccountEmailLink").ToTimeLimitedDataProtector();
            _dbContext = dbContext;
            _logger = logger;
            _emailService = emailService;
            _hostEnvironment = hostEnvironment;
            _contentProvider = contentProvider;
            _linkGenerator = linkGenerator;
            _userAccountStore = userAccountStore;
        }

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
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CheckYourEmailAsync(string email, CancellationToken token = default)
        {
            var code = _dataProtector.Protect(email, TimeSpan.FromHours(24));

            var link = _linkGenerator.GetUriByName(HttpContext, nameof(VerifyEmailAsync), new { code });

            var emailKey = await HasAccount(email, token)
                ? "email-rms-registration-attempt"
                : "email-rms-new-registration";

            var emailContent = await GetEmailContent(emailKey, new { recipientEmail = email, link }, token);

            await _emailService.SendEmailAsync(email, emailContent.EmailSubject, emailContent.EmailBody, token);

            if (_hostEnvironment.IsDevelopment())
            {
                ViewData["email-to"] = email;
                ViewData["email-subject"] = emailContent.EmailSubject;
                ViewData["email-body"] = emailContent.EmailBody;
                ViewData["email-link"] = link;
            }

            return View(model: email);
        }

        private async Task<EmailTemplate> GetEmailContent<TData>(string templateName, TData data, CancellationToken token)
        {
            var source = await _contentProvider.GetContentAsync<EmailTemplate>(templateName, token);

            if (!string.IsNullOrWhiteSpace(source.EmailBody))
            {
                var template = Handlebars.Compile(source.EmailBody);


                source.EmailBody = Dte.Common.Content.CustomMessageEmail.GetCustomMessageHtml().Replace("###BODY_REPLACE###", template(data));
            }

            if (!string.IsNullOrWhiteSpace(source.EmailSubject))
            {
                var template = Handlebars.Compile(source.EmailSubject);
                source.EmailSubject = template(data);
            }

            return source;
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
                    var homeUrl = _linkGenerator.GetUriByAction(HttpContext, nameof(HomeController.Index), "Home") + "?sign-in";
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
                _logger.LogWarning(ex, ex?.Message);
            }

            ViewData.AddNotification(new NotificationBannerModel
            {
                Heading = "Email verification error",
                Body = "There was a problem with your email verification code.",
                IsSuccess = false,
                SubBodyText = "You can try to resend the email below."
            });

            return RedirectToAction(nameof(Create));
        }

        private async Task<bool> HasAccount(string email, CancellationToken token)
        {
            var hasAccount = await _dbContext.User
                                .IgnoreQueryFilters() // check for active and deleted accounts.
                                .AnyAsync(x => x.ContactEmail == email.Trim().ToLowerInvariant(), token);

            hasAccount = hasAccount || await _userAccountStore.UserWithEmailExistsAsync(email, token);
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
                _logger.LogWarning(ex, ex?.Message);
            }

            ViewData.AddNotification(new NotificationBannerModel
            {
                Heading = "Email verification error",
                Body = "There was a problem with your email verification code.",
                IsSuccess = false,
                SubBodyText = "You can try to resend the email below."
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
                        ViewData.AddNotification(new NotificationBannerModel
                        {
                            Heading = "Account already exists",
                            Body = "An account already exists for your email address",
                            IsSuccess = false,
                            SubBodyText = "Sign in from the RMS homepage"
                        });

                        return View(model);
                    }

                    var userId = await _userAccountStore.CreateNewUserAsync(email, model.FirstName, model.LastName, model.Password, token);

                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        var user = _dbContext.User.Add(new User
                        {
                            AuthenticationId = userId,
                            ContactEmail = email,
                            ContactFullName = $"{model.FirstName} {model.LastName}"
                        });

                        _dbContext.UserRole.Add(new UserRole { User = user.Entity, RoleId = RoleConfiguration.RoleId("Researcher") });
                        await _dbContext.SaveChangesAsync(token);

                        return View("New");
                    }
                    else
                    {
                        ViewData.AddNotification(new NotificationBannerModel
                        {
                            Heading = "Unable to create user account",
                            Body = "Unable to create user account",
                            IsSuccess = false,
                        });
                    }
                }

                return View(model);
            }
            catch (CryptographicException ex)
            {
                // Code is invalid or has expired.
                _logger.LogWarning(ex, ex?.Message);
            }

            ViewData.AddNotification(new NotificationBannerModel
            {
                Heading = "Email verification error",
                Body = "There was a problem with your email verification code.",
                IsSuccess = false,
                SubBodyText = "You can try to resend the email below."
            });

            return RedirectToAction(nameof(Create));
        }
    }
}
