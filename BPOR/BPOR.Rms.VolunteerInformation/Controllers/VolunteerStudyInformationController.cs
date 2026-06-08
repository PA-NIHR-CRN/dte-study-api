using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models;
using BPOR.Rms.VolunteerInformation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore.Validation;
using NIHR.Rts.Client;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[AllowAnonymous]
[Route("Study/{studyId:int}/VolunteerInformation/[action]")]
public class VolunteerStudyInformationController : VsiControllerBase
{

    public VolunteerStudyInformationController(IVsiRepository vsiRepository) : base(vsiRepository)
    {
    }
    // static VolunteerStudyInformationController()
    // {
    //     var metadata = new MultistepFormBuilder<VsiEditModel>()
    //         .AddSection("Section 1. Overview", section => section
    //             .AddPropertyStep("About the Study", step => step
    //                 .AddProperty(i => i.Description))
    //             .AddPropertyStep("Select the type of study", step => step
    //                 .AddRadioProperty(i => i.StudyType, radio => radio
    //                     .AddOption(VsiStudyTypeId.InPerson, "In Person")
    //                     .AddOption(VsiStudyTypeId.Remote, "Remote")
    //                     .AddOption(VsiStudyTypeId.Hybrid, "Hybrid"))
    //                 .WithPostAction("SelectStudyType"))
    //             .AddCustomActionStep("ResearchLocation", step => step
    //                 .ShowOnlyIf(i => i.StudyType is VsiStudyTypeId.InPerson or VsiStudyTypeId.Hybrid))
    //             .AddCustomActionStep("VolunteerGroups"))
    //         .AddSection("Section 2: What does the study involve?", section => section
    //             .AddPropertyStep("What you will do", step => step
    //                 .WithDescription(
    //                     "A brief explanation about what the volunteer is expected to do immediately after recruitment")
    //                 .AddProperty(i => i.WhatWillYouDo))
    //             .AddPropertyStep("Will volunteers be offered an incentive for taking part?", step => step
    //                 .AddYesNoProperty(i => i.HasIncentive)
    //                 .AddProperty(i => i.IncentiveDetails, prop => prop
    //                     .WithCaption(
    //                         "If yes please provide details of the incentives the volunteer will receive for taking part.")))
    //         ).Build();
    // }

    #region Start

    [HttpGet]
    [DoNotRequireVsi]
    public IActionResult Start()
    {
        return View();
    }

    [HttpPost]
    [DoNotRequireVsi]
    public async Task<IActionResult> Start(
        [FromServices] IStudyRepository studyRepository,
        int studyId,
        CancellationToken cancellationToken)
    {
        var study = await studyRepository.GetStudy(studyId, cancellationToken);
        if (study == null)
        {
            return NotFound();
        }

        var currentVsi = (await VsiRepository.GetCurrentVsi(studyId, cancellationToken));
        if (currentVsi != null)
        {
            // TODO: there is an existing active draft ... Cope with this better!!
            return BadRequest();
        }

        await VsiRepository.CreateVsi(studyId, VolunteerStudyInformationStatusId.Draft, cancellationToken);
        
        return RedirectToAction("Section1_Step1", new { studyId = studyId });
    }

    public async Task<IActionResult> Resume(int studyId, CancellationToken cancellationToken)
    {
        var currentVsi = await VsiRepository.GetCurrentVsi(studyId, cancellationToken);
        return currentVsi == null
            ? NotFound()
            :
            // TODO: figure out which step to resume on.
            RedirectToAction("Section1_Step1", new { studyId });
    }

    #endregion

    #region Section1
    #region Section1_Step1

    [HttpGet]
    public async Task<IActionResult> Section1_Step1(int studyId, CancellationToken cancellationToken)
    {
        var data = await VsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                Description = i.Description
            },
            cancellationToken);

        return data == null ? NotFound() : View(data);
    }


    [HttpPost]
    public async Task<IActionResult> Section1_Step1(
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        new VsiValidator().ValidateSpecificProperties(model, i => i.Description).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await VsiRepository.UpdateVsi(studyId, i => i.Description = model.Description, cancellationToken);
        return RedirectToAction("Section1_Step2", new { studyId });
    }

    #endregion

    #region Section1_Step2

    [HttpGet]
    public async Task<IActionResult> Section1_Step2(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                StudyType = i.StudyTypeId
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section1_Step2(
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        new VsiValidator().ValidateSpecificProperties(model, i => i.StudyType).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VsiRepository.UpdateVsi(studyId, i => i.StudyTypeId = model.StudyType, cancellationToken);
        return RedirectToAction("Section1_Step3", new { studyId });
    }

    #endregion

    #region Section1_Step3

    [HttpGet]
    public async Task<IActionResult> SiteSearch(
        [FromServices] IRtsAddressSource addressSource,
        [FromRoute] int studyId,
        string searchTerm,
        CancellationToken cancellationToken)
    {
        SiteSearchModel model = new SiteSearchModel { SearchTerm = searchTerm };
        ModelState.Clear();
        new SiteSearchModelValidator().ValidateSpecificProperties(model, i => i.SearchTerm).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.SearchResult = (await addressSource.SearchByPostcode(model.SearchTerm, cancellationToken)).ToArray();
        if (model.SearchResult.Length == 0)
        {
            ModelState.AddModelError(nameof(SiteSearchModel.SearchTerm),
                "The postcode you've entered cannot be found.");
        }


        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SiteSearch(
        [FromServices] IRtsAddressSource addressSource,
        [FromRoute] int studyId,
        SiteSearchModel model,
        CancellationToken cancellationToken)
    {
        ModelState.Clear();
        new SiteSearchModelValidator().ValidateSpecificProperties(model, i => i.SelectedRtsId)
            .AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            model.SearchResult = (await addressSource.SearchByPostcode(model.SearchTerm, cancellationToken)).ToArray();
            return View(model);
        }

        var result = await addressSource.GetById(model.SelectedRtsId!.Value, cancellationToken);
        if (result == null)
        {
            ModelState.AddModelError(nameof(model.SelectedRtsId), "The selected address has been removed from RTS");
            return View(model);
        }

        return await AddSite(result, studyId, cancellationToken);
    }

    private async Task<IActionResult> AddSite(RtsAddress addressToAdd, int studyId, CancellationToken cancellationToken)
    {
        var site = new VolunteerStudyInformationSite()
        {
            AddressLine1 = addressToAdd.AddressLine1,
            AddressLine2 = addressToAdd.AddressLine2,
            AddressLine3 = addressToAdd.AddressLine3,
            AddressLine4 = addressToAdd.AddressLine4,
            AddressLine5 = addressToAdd.AddressLine5,
            Postcode = addressToAdd.Postcode
        };
        int? result = await VsiRepository.CreateSite(studyId, site, cancellationToken);
        return result.HasValue ? RedirectToAction("Section1_Step3", new { studyId }) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Section1_Step3(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveSite(int studyId, int siteId, CancellationToken cancellationToken)
    {
        bool result = await VsiRepository.RemoveSite(studyId, siteId, cancellationToken);
        return result ? RedirectToAction("Section1_Step3", new { studyId, id = siteId }) : NotFound();
    }

    [HttpGet]
    public IActionResult ManualSiteEntry(int studyId)
    {
        return View(new ManualSiteEntryModel { StudyId = studyId });
    }

    [HttpPost]
    public async Task<IActionResult> ManualSiteEntry(
        int studyId,
        [FromForm] ManualSiteEntryModel model,
        CancellationToken cancellationToken)
    {
        ModelState.Clear();
        (await new ManualSiteEntryModelValidator().ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        return await AddSite(model.Address, studyId, cancellationToken);
    }

    #endregion

    #region Section1_Step4

    [HttpGet]
    public async Task<IActionResult> Section1_Step4(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                Groups = i.Groups.Select(g => new VsiGroupModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Criteria = g.Criteria.Select(c => new VsiGroupCriteriaModel()
                    {
                        Criteria = c.Criteria,
                        Type = c.TypeId
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

        return RedirectToAction("CreateGroup", new { studyId });
    }


    [HttpGet]
    public async Task<IActionResult> CreateGroup(int studyId, CancellationToken cancellationToken)
    {
        return View(new CreateGroupModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(
        int studyId,
        [FromForm] CreateGroupModel model,
        CancellationToken cancellationToken)
    {
        (await new CreateGroupModelValidator().ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        int? groupId = await VsiRepository.CreateGroup(studyId, model.Name, cancellationToken);
        return groupId == null
            ? NotFound()
            : RedirectToAction("CreateCriterion", "Group",
                new { studyId, groupId, type = VolunteerStudyInformationGroupCriteriaTypeId.Include });
    }

    #endregion
    #endregion
    
    #region Section2
    #region Section2_Step1

    [HttpGet]
    public async Task<IActionResult> Section2_Step1(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.WhatYouWillDo).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VsiRepository.UpdateVsi(studyId, vsi => vsi.WhatYouWillDo = model.WhatYouWillDo,
            cancellationToken);
        return RedirectToAction("Section2_Step2", new { studyId });
    }

    #endregion

    #region Section2_Step2

    [HttpGet]
    public async Task<IActionResult> Section2_Step2(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.CostReimbursement).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VsiRepository.UpdateVsi(studyId, vsi => vsi.CostReimbursement = model.CostReimbursement,
            cancellationToken);
        return RedirectToAction("Section2_Step3", new { studyId });
    }

    #endregion

    #region Section2_Step3

    [HttpGet]
    public async Task<IActionResult> Section2_Step3(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.IncentiveDetails, i => i.HasIncentive).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VsiRepository.UpdateVsi(studyId, vsi =>
            {
                vsi.IncentiveDetails = model.IncentiveDetails;
                vsi.HasIncentive = model.HasIncentive;
            },
            cancellationToken);
        return RedirectToAction("Section2_Step4", new { studyId });
    }

    #endregion

    #region Section2_Step4

    [HttpGet]
    public async Task<IActionResult> Section2_Step4(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.NumberOfVisits).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await VsiRepository.UpdateVsi(studyId, vsi => vsi.NumberOfVisits = model.NumberOfVisits,
            cancellationToken);
        return RedirectToAction("Section2_Step5", new { studyId });
    }

    #endregion

    #region Section2_Step5

    [HttpGet]
    public async Task<IActionResult> Section2_Step5(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.StudyDuration).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VsiRepository.UpdateVsi(studyId, vsi => vsi.StudyDuration = model.StudyDuration,
            cancellationToken);
        return RedirectToAction("Section2_Step6", new { studyId });
    }

    #endregion

    #region Section2_Step6

    [HttpGet]
    public async Task<IActionResult> Section2_Step6(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.StudyFormat).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VsiRepository.UpdateVsi(studyId, vsi => vsi.StudyFormat = model.StudyFormat,
            cancellationToken);
        return RedirectToAction("Section2_Step7", new { studyId });
    }

    #endregion

    #region Section2_Step7

    [HttpGet]
    public async Task<IActionResult> Section2_Step7(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.OtherDetails).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await VsiRepository.UpdateVsi(studyId, vsi => vsi.OtherDetails = model.OtherDetails,
            cancellationToken);
        return RedirectToAction("Section3_Step1", new { studyId });
    }

    #endregion
    
    #endregion
    #region Section3

    #region Section3_Step1

    [HttpGet]
    public async Task<IActionResult> Section3_Step1(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                StagedPreScreenerUrl = i.StagedPreScreenerUrl
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section3_Step1(
        [FromRoute] int studyId,
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

        await VsiRepository.UpdateVsi(studyId, vsi => vsi.StagedPreScreenerUrl = model.StagedPreScreenerUrl,
            cancellationToken);
        return RedirectToAction("Section3_Step2", new { studyId });
    }

    #endregion

    #region Section3_Step2

    [HttpGet]
    public async Task<IActionResult> Section3_Step2(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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

        await VsiRepository.UpdateVsi(studyId, vsi => vsi.ExternalWebsiteUrl = model.ExternalWebsiteUrl,
            cancellationToken);
        return RedirectToAction("Section3_Step3", new { studyId });
    }

    #endregion

    #region Section3_Step3

    [HttpGet]
    public async Task<IActionResult> Section3_Step3(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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
        
        return RedirectToAction("CreateContact", new { studyId });
    }

    [HttpGet]
    public IActionResult CreateContact(int studyId)
    {
        return View(new VsiContactModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateContact(int studyId, VsiContactModel model, CancellationToken cancellationToken)
    {
        ModelState.Clear();
        (await new VsiContactModelValidator().ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await VsiRepository.CreateContact(studyId, new VolunteerStudyInformationContact
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
        await VsiRepository.RemoveContact(studyId, contactId, cancellationToken);
        
        return RedirectToAction("Section3_Step3", new { studyId });
    }
    
    #endregion

    #region Section3_Step4

    [HttpGet]
    public async Task<IActionResult> Section3_Step4(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
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

        await VsiRepository.UpdateVsi(studyId, vsi => vsi.InfoToRegisterByEmail = model.InfoToRegisterByEmail,
            cancellationToken);
        return RedirectToAction("Section4", new { studyId });
    }

    #endregion

    #endregion

    [HttpGet]
    public async Task<IActionResult> Section4(int studyId, CancellationToken cancellationToken)
    {
        var model = await VsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                Description = i.Description,
                StudyType = i.StudyTypeId,
               WhatYouWillDo = i.WhatYouWillDo,
               CostReimbursement = i.CostReimbursement,
               HasIncentive = i.CostReimbursement,
               IncentiveDetails  = i.IncentiveDetails,
               NumberOfVisits = i.NumberOfVisits,
               StudyDuration = i.StudyDuration,
               StudyFormat = i.StudyFormat,
               OtherDetails  = i.OtherDetails,
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
                        Criteria = c.Criteria,
                        Type = c.TypeId
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
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section4(int studyId, CancellationToken cancellationToken, bool commit = true)
    {
        await VsiRepository.UpdateVsi(studyId, i => i.StatusId = VolunteerStudyInformationStatusId.Active, cancellationToken);
        return RedirectToAction("NextSteps", new { studyId });
    }

    public async Task<IActionResult> NextSteps(int studyId, CancellationToken cancellationToken)
    {
        return View();
    }
}