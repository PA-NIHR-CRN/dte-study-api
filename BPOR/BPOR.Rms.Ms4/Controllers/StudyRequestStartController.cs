using System.Globalization;
using BPOR.Domain.Entities;
using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

namespace BPOR.Rms.Ms4.Controllers;

[Route("studyRequest/[action]")]
[AllowAnonymous]
public class StudyRequestStartController(IStudyDraftRepository studyDraftRepository)
    : Controller
{
    [HttpGet]
    public IActionResult Start()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Start(
        StudyRequestStartViewModel model,
        [FromServices] IUrlAccessTokenService urlAccessTokenService,
        [FromServices] IValidator<StudyRequestStartViewModel> validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View(model);
        }

        var study = new Study();
        var studyId = await studyDraftRepository.CreateDraftStudyAsync(study, cancellationToken);
        
        var uri = Url.GetUrl(StudyRequestEditFlow.EthicsApproval, new StudyRequestEditContext
        {
            StudyId = studyId,
            FlowType = StudyRequestEditFlowType.ResearcherCreate
        });

        var token = new AccessToken(AccessTokenRoleNames.ResearcherCreateStudy)
            .WithRoute(
                nameof(StudyRequestEditContext.StudyId),
                studyId.ToString(CultureInfo.InvariantCulture));
        
        uri = urlAccessTokenService.AddAccessToken(uri!, token);

        return Redirect(uri);
    }
    
    [HttpGet]
    public IActionResult ApplicationSubmitted(int studyId)
    {
        return View();
    }
    
    private async Task<bool> ValidateAsync<TModel>(
        IValidator<TModel> validator,
        TModel model,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(model, cancellationToken);
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return validationResult.IsValid;
    }
}