using BPOR.Rms.Abstractions.Entities;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models;
using BPOR.Rms.VolunteerInformation.Settings;
using BPOR.Rms.VolunteerInformation.Tokens;
using BPOR.Rms.VolunteerInformation.Validators;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.AspNetCore.Validation;
using NIHR.Rts.Client;
using Rbec.Postcodes;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[Route("Study/{studyId:int}/VolunteerInformation/[action]")]
public class VolunteerInformationPageController : VipControllerBase<VsiEditContext>
{
    public VolunteerInformationPageController(IVipRepository vipRepository) : base(vipRepository)
    {
    }

    #region Section1

    #region Section1_Step1

    [HttpGet]
    public async Task<IActionResult> Section1_Step1(int studyId, VipFlowMode flowMode,
        CancellationToken cancellationToken)
    {
        var data = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                FlowMode = flowMode,
                Description = i.Description
            },
            cancellationToken);

        return data == null ? NotFound() : View(data);
    }


    [HttpPost]
    public async Task<IActionResult> Section1_Step1(
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        new VsiValidator().ValidateSpecificProperties(model, i => i.Description).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(EditContext.StudyId, i => i.Description = model.Description, cancellationToken);

        return RedirectNextStep("Section1_Step2");
    }

    #endregion

    #region Section1_Step2

    [HttpGet]
    public async Task<IActionResult> Section1_Step2(int studyId, VipFlowMode flowMode,
        CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                StudyType = i.StudyType,
                FlowMode = flowMode
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section1_Step2(
        [FromRoute] int studyId,
        VipFlowMode flowMode,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        new VsiValidator().ValidateSpecificProperties(model, i => i.StudyType).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, i => i.StudyType = model.StudyType, cancellationToken);

        return model.StudyType switch
        {
            VsiStudyType.Remote => RedirectNextStep("Section1_Step4"),
            VsiStudyType.InPerson or VsiStudyType.Hybrid => RedirectToAction("Section1_Step3"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    #endregion

    #region Section1_Step3

    [HttpGet]
    public async Task<IActionResult> SiteSearch(
        [FromServices] IRtsAddressSource addressSource,
        [FromRoute] int studyId,
        SiteSearchModel model,
        CancellationToken cancellationToken)
    {
        ModelState.Clear();
        new SiteSearchResultsModelValidator().ValidateSpecificProperties(model, i => i.SearchTerm)
            .AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.IsSearching)
        {
            var parsedPostcode = Postcode.Parse(model.SearchTerm);

            model.SearchResult =
                (await addressSource.SearchByPostcode(parsedPostcode, cancellationToken)).ToArray();

            if (model.SearchResult.Length == 0)
            {
                ModelState.AddModelError(nameof(SiteSearchModel.SearchTerm),
                    "The postcode you've entered cannot be found.");
            }
        }

        return View(model);
    }

    [HttpPost]
    [ActionName(nameof(SiteSearch))]
    public async Task<IActionResult> SiteSearchPostBack(
        [FromServices] IRtsAddressSource addressSource,
        [FromRoute] int studyId,
        SiteSearchModel model,
        CancellationToken cancellationToken)
    {
        ModelState.Clear();
        new SiteSearchResultsModelValidator().ValidateSpecificProperties(model, i => i.SelectedRtsId)
            .AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            var postcode = Postcode.Parse(model.SearchTerm);
            model.SearchResult = (await addressSource.SearchByPostcode(postcode, cancellationToken)).ToArray();
            return View(model);
        }

        var result = await addressSource.GetById(
            model.SelectedRtsId,
            cancellationToken);

        if (result == null)
        {
            ModelState.AddModelError(nameof(model.SelectedRtsId), "The selected address has been removed from RTS");
            return View(model);
        }

        return await AddSite(result, cancellationToken);
    }

    private async Task<IActionResult> AddSite(RtsAddress addressToAdd, CancellationToken cancellationToken)
    {
        var site = new VsiSite()
        {
            AddressLine1 = addressToAdd.AddressLine1,
            AddressLine2 = addressToAdd.AddressLine2,
            AddressLine3 = addressToAdd.AddressLine3,
            AddressLine4 = addressToAdd.AddressLine4,
            AddressLine5 = addressToAdd.AddressLine5,
            Postcode = addressToAdd.Postcode
        };
        int? result = await VipRepository.CreateSite(EditContext.StudyId, site, cancellationToken);
        return result.HasValue ? RedirectToAction("Section1_Step3", EditContext) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Section1_Step3(int studyId, VipFlowMode flowMode, bool isNavigateBack,
        CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                Sites = i.Sites.Select(site => new VsiSiteModel
                {
                    AddressLine1 = site.AddressLine1,
                    AddressLine2 = site.AddressLine2,
                    AddressLine3 = site.AddressLine3,
                    AddressLine4 = site.AddressLine4,
                    AddressLine5 = site.AddressLine5,
                    Postcode = site.Postcode,
                    Id = site.Id
                })
            },
            cancellationToken);

        if (model == null)
        {
            return NotFound();
        }

        if (model.Sites.Any())
        {
            return View(model);
        }

        if (isNavigateBack)
        {
            return RedirectNextStep("Section1_Step2");
        }

        return RedirectToAction("SiteSearch", EditContext);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveSite(int studyId, int siteId, CancellationToken cancellationToken)
    {
        bool result = await VipRepository.RemoveSite(studyId, siteId, cancellationToken);
        return result ? RedirectToAction("Section1_Step3", new { studyId, id = siteId }) : NotFound();
    }

    [HttpGet]
    public IActionResult ManualSiteEntry(int studyId)
    {
        return View(new ManualSiteEntryModel { StudyId = studyId });
    }

    [HttpPost]
    public async Task<IActionResult> ManualSiteEntry(
        [FromForm] ManualSiteEntryModel model,
        CancellationToken cancellationToken)
    {
        ModelState.Clear();
        (await new ManualSiteEntryModelValidator().ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        return await AddSite(model.Address, cancellationToken);
    }

    #endregion

    #region Section1_Step4

    [HttpGet]
    public async Task<IActionResult> Section1_Step4(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                Groups = i.Groups.Select(g => new VsiGroupModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Criteria = g.Criteria.Select(c => new VsiGroupCriteriaModel()
                    {
                        Description = c.Description,
                        Type = c.Type
                    })
                })
            },
            cancellationToken);

        if (model == null)
        {
            return NotFound();
        }

        if (model.Groups.Any())
        {
            return View(model);
        }

        return RedirectToAction("CreateGroup", EditContext);
    }


    [HttpGet]
    public async Task<IActionResult> CreateGroup(int studyId, CancellationToken cancellationToken)
    {
        return View(new CreateGroupModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(
        [FromForm] CreateGroupModel model,
        CancellationToken cancellationToken)
    {
        (await new CreateGroupModelValidator().ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        int? groupId = await VipRepository.CreateGroup(EditContext.StudyId, model.Name, cancellationToken);
        return groupId == null
            ? NotFound()
            : RedirectToAction("CreateGroupCheck", "VolunteerInformationGroup",
                new { EditContext.StudyId, EditContext.FlowMode, groupId });
    }

    #endregion

    #endregion

    #region Section2

    #region Section2_Step1

    [HttpGet]
    public async Task<IActionResult> Section2_Step1(int studyId, VipFlowMode flowMode,
        CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                WhatYouWillDo = i.WhatYouWillDo
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Section2_Step1(
        [FromRoute] int studyId,
        VipFlowMode flowMode,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.WhatYouWillDo).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, vsi => vsi.WhatYouWillDo = model.WhatYouWillDo,
            cancellationToken);

        return RedirectNextStep("Section2_Step2");
    }

    #endregion

    #region Section2_Step2

    [HttpGet]
    public async Task<IActionResult> Section2_Step2(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                CostReimbursement = i.CostReimbursement
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Section2_Step2(
        [FromRoute] int studyId,
        VipFlowMode flowMode,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.CostReimbursement).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, vsi => vsi.CostReimbursement = model.CostReimbursement,
            cancellationToken);

        return RedirectNextStep("Section2_Step3");
    }

    #endregion

    #region Section2_Step3

    [HttpGet]
    public async Task<IActionResult> Section2_Step3(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                IncentiveDetails = i.IncentiveDetails,
                HasIncentive = i.HasIncentive
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step3(
        [FromRoute] int studyId,
        VipFlowMode flowMode,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.IncentiveDetails, i => i.HasIncentive)
            .AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, vsi =>
            {
                vsi.IncentiveDetails = model.IncentiveDetails;
                vsi.HasIncentive = model.HasIncentive;
            },
            cancellationToken);

        return RedirectNextStep("Section2_Step4");
    }

    #endregion

    #region Section2_Step4

    [HttpGet]
    public async Task<IActionResult> Section2_Step4(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                NumberOfVisits = i.NumberOfVisits
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step4(
        [FromRoute] int studyId,
        VipFlowMode flowMode,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.NumberOfVisits).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, vsi => vsi.NumberOfVisits = model.NumberOfVisits,
            cancellationToken);

        return RedirectNextStep("Section2_Step5");
    }

    #endregion

    #region Section2_Step5

    [HttpGet]
    public async Task<IActionResult> Section2_Step5(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                StudyDuration = i.StudyDuration
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step5(
        [FromRoute] int studyId,
        VipFlowMode flowMode,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.StudyDuration).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, vsi => vsi.StudyDuration = model.StudyDuration,
            cancellationToken);

        return RedirectNextStep("Section2_Step6");
    }

    #endregion

    #region Section2_Step6

    [HttpGet]
    public async Task<IActionResult> Section2_Step6(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                StudyFormat = i.StudyFormat
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step6(
        [FromRoute] int studyId,
        VipFlowMode flowMode,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.StudyFormat).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, vsi => vsi.StudyFormat = model.StudyFormat,
            cancellationToken);

        return RedirectNextStep("Section2_Step7");
    }

    #endregion

    #region Section2_Step7

    [HttpGet]
    public async Task<IActionResult> Section2_Step7(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                OtherDetails = i.OtherDetails
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step7(
        [FromRoute] int studyId,
        VipFlowMode flowMode,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.OtherDetails).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, vsi => vsi.OtherDetails = model.OtherDetails,
            cancellationToken);

        return RedirectNextStep("Section3_Step1");
    }

    #endregion

    #endregion

    #region Section3

    #region Section3_Step1

    [HttpGet]
    public async Task<IActionResult> Section3_Step1(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                StagedPreScreenerUrl = i.StagedPreScreenerUrl
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section3_Step1(
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.StagedPreScreenerUrl).AddToModelState(ModelState);

        // Do this properly - this property is optional in general but mandatory on this one action.
        if (string.IsNullOrWhiteSpace(model.StagedPreScreenerUrl))
        {
            ModelState.AddModelError(nameof(model.StagedPreScreenerUrl),
                "Enter information to Continue. If this is not relevant to your study, skip this question.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(EditContext.StudyId, vsi => vsi.StagedPreScreenerUrl = model.StagedPreScreenerUrl,
            cancellationToken);

        return RedirectNextStep("Section3_Step2");
    }

    private new IActionResult RedirectToAction([AspMvcAction] string action)
        => RedirectToAction(action, EditContext);

    private IActionResult RedirectNextStep([AspMvcAction] string action)
        => RedirectNextStep(action, EditContext);

    private IActionResult RedirectNextStep([AspMvcAction] string action, [AspMvcModelType] object route)
    {
        return EditContext.FlowMode switch
        {
            VipFlowMode.Create => RedirectToAction(action, route),
            VipFlowMode.Edit => RedirectToAction("Section4"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private IActionResult RedirectNextStep(IActionResult createFlow)
    {
        return EditContext.FlowMode switch
        {
            VipFlowMode.Create => createFlow,
            VipFlowMode.Edit => RedirectToAction("Section4", EditContext),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    #endregion

    #region Section3_Step2

    [HttpGet]
    public async Task<IActionResult> Section3_Step2(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                ExternalWebsiteUrl = i.ExternalWebsiteUrl
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section3_Step2(
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.ExternalWebsiteUrl).AddToModelState(ModelState);

        // Do this properly - this property is optional in general but mandatory on this one action.
        if (string.IsNullOrWhiteSpace(model.ExternalWebsiteUrl))
        {
            ModelState.AddModelError(nameof(model.ExternalWebsiteUrl),
                "Enter information to Continue. If this is not relevant to your study, skip this question.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, vsi => vsi.ExternalWebsiteUrl = model.ExternalWebsiteUrl,
            cancellationToken);

        return RedirectNextStep(RedirectToAction("Section3_Step3", EditContext));
    }

    #endregion

    #region Section3_Step3

    [HttpGet]
    public async Task<IActionResult> Section3_Step3(int studyId, bool isNavigateBack,
        CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                Contacts = i.Contacts.Select(c => new VsiContactModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Role = c.Role,
                    Organisation = c.Organisation,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber
                })
            },
            cancellationToken);
        if (model == null)
        {
            return NotFound();
        }

        if (model.Contacts.Any())
        {
            return View(model);
        }
        
        if (isNavigateBack)
        {
            return RedirectNextStep("Section3_Step2");
        }

        return RedirectToAction("CreateContact", EditContext);
    }

    [HttpGet]
    public IActionResult CreateContact(int studyId)
    {
        return View(new VsiContactModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(int studyId, VsiContactModel model,
        CancellationToken cancellationToken)
    {
        ModelState.Clear();
        (await new VsiContactModelValidator().ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.CreateContact(studyId, new VsiContact
        {
            Name = model.Name,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Organisation = model.Organisation,
            Role = model.Role
        }, cancellationToken);

        return RedirectToAction("Section3_Step3", new { studyId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveContact(int studyId, int contactId, CancellationToken cancellationToken)
    {
        await VipRepository.RemoveContact(studyId, contactId, cancellationToken);

        return RedirectToAction("Section3_Step3", new { studyId });
    }

    #endregion

    #region Section3_Step4

    [HttpGet]
    public async Task<IActionResult> Section3_Step4(int studyId, CancellationToken cancellationToken)
    {
        var model = await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                InfoToRegisterByEmail = i.InfoToRegisterByEmail
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section3_Step4(
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.InfoToRegisterByEmail).AddToModelState(ModelState);

        // Do this properly - this property is optional in general but mandatory on this one action.
        if (string.IsNullOrWhiteSpace(model.InfoToRegisterByEmail))
        {
            ModelState.AddModelError(nameof(model.InfoToRegisterByEmail),
                "Enter information to Continue. If this is not relevant to your study, skip this question.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, vsi => vsi.InfoToRegisterByEmail = model.InfoToRegisterByEmail,
            cancellationToken);
        return RedirectToAction("Section4", new { studyId });
    }

    #endregion

    #endregion

    [HttpGet]
    public async Task<IActionResult> Section4(int studyId, CancellationToken cancellationToken)
    {
        var model = await GetFullPageModel(studyId, cancellationToken);
        if (model == null)
        {
            return NotFound();
        }
        
        ModelState.Clear();
        (await new VsiValidator().ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);
        return  View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section4(int studyId, CancellationToken cancellationToken, bool commit = true)
    {
        var model = await GetFullPageModel(studyId, cancellationToken);
        if (model == null)
        {
            return NotFound();
        }
        
        ModelState.Clear();
        (await new VsiValidator().ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VipRepository.UpdateVsi(studyId, i => i.Status = VsiStatus.Active, cancellationToken);
        return RedirectToAction("NextSteps", new { studyId });
    }

    private async Task<VsiEditModel?> GetFullPageModel(int studyId, CancellationToken cancellationToken)
    {
        return await VipRepository.GetPage(studyId,
            i => new VsiEditModel
            {
                Description = i.Description,
                StudyType = i.StudyType,
                WhatYouWillDo = i.WhatYouWillDo,
                CostReimbursement = i.CostReimbursement,
                HasIncentive = i.HasIncentive,
                IncentiveDetails = i.IncentiveDetails,
                NumberOfVisits = i.NumberOfVisits,
                StudyDuration = i.StudyDuration,
                StudyFormat = i.StudyFormat,
                OtherDetails = i.OtherDetails,
                ExternalWebsiteUrl = i.ExternalWebsiteUrl,
                InfoToRegisterByEmail = i.InfoToRegisterByEmail,
                StagedPreScreenerUrl = i.StagedPreScreenerUrl,

                Contacts = i.Contacts.Select(c => new VsiContactModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Role = c.Role,
                    Organisation = c.Organisation,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber
                }),
                Groups = i.Groups.Select(g => new VsiGroupModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Criteria = g.Criteria.Select(c => new VsiGroupCriteriaModel
                    {
                        Id = c.Id,
                        Description = c.Description,
                        Type = c.Type
                    })
                }),
                Sites = i.Sites.Select(s => new VsiSiteModel
                {
                    AddressLine1 = s.AddressLine1,
                    AddressLine2 = s.AddressLine2,
                    AddressLine3 = s.AddressLine3,
                    AddressLine4 = s.AddressLine4,
                    AddressLine5 = s.AddressLine5,
                    Postcode = s.Postcode,
                    Id = s.Id
                })
            },
            cancellationToken);
    }

    public IActionResult NextSteps()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Resume(int studyId, CancellationToken cancellationToken)
    {
        var currentVsi = await VipRepository.GetPage(studyId, cancellationToken);
        return currentVsi == null
            ? NotFound()
            :
            // TODO: figure out which step to resume on.
            RedirectToAction("Section1_Step1", new { studyId, flowMode = VipFlowMode.Create });
    }

    [HttpPost]
    public async Task<IActionResult> Reset(int studyId, CancellationToken cancellationToken)
    {
        await VipRepository.ResetPage(studyId, cancellationToken);
        return RedirectToAction("Section1_Step1", new { studyId, flowMode = VipFlowMode.Create });
    }
    
    public IActionResult PreviewVip(
        [FromServices] IVipTokenGenerator tokenGenerator,
        [FromServices] IOptions<VipSettings> options,
        int studyId)
    {
        var uri = QueryHelpers.AddQueryString(options.Value.BporVipUri, new Dictionary<string, string?>
        {
            ["token"] = tokenGenerator.GenerateToken(VipTokenPurpose.AdminPreview, studyId),
        });

        return Redirect(uri);
    }
}