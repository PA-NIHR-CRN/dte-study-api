using BPOR.Rms.Abstractions.Entities;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models;
using BPOR.Rms.VolunteerInformation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[AllowAnonymous]
[Route("Study/{studyId:int}/VolunteerInformation/groups/{groupId:int}/[action]")]
public class VolunteerInformationGroupController(IVipRepository vipRepository) : VipControllerBase<VsiGroupEditContext>(vipRepository)
{
    protected override Task<IActionResult?> InitialiseEditContext(ActionExecutingContext context, VsiGroupEditContext editContext,
        CancellationToken cancellationToken)
    {
        var groupIdValue = context.RouteData.Values["groupId"];
        if (groupIdValue is string groupIdString 
            && int.TryParse(groupIdString, out var groupId))
        {
            editContext.GroupId = groupId;
        }
        return base.InitialiseEditContext(context, editContext, cancellationToken);
    }

    [HttpGet]
    public async Task<IActionResult> CreateCriterion(VsiGroupCriteronType type, CancellationToken cancellationToken)
    {
        var group = await vipRepository.GetGroup(EditContext.StudyId, EditContext.GroupId,
            i => new VsiGroupModel()
            {
                Id = i.Id,
                Name = i.Name
            }
            , cancellationToken);

        return View(new CreateCriterionModel{GroupName = group.Name, Type = type});
    }

    [HttpPost]
    public async Task<IActionResult> CreateCriterion(
        int studyId,
        int groupId,
        VsiGroupCriteronType type,
        [FromForm] CreateCriterionPostbackModel model,
        CancellationToken cancellationToken)
    {
        var group = await vipRepository.GetGroup(studyId, groupId, i => i, cancellationToken);

        (await new CreateCriterionModelValidator().ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View(new CreateCriterionModel{Criterion = model.Criterion, GroupName = group.Name, Type = type});
        }

        var newCriteria = new VsiGroupCriterion()
        {
            Description = model.Criterion,
            Type = type
        };
        
        await vipRepository.CreateCriterion(studyId, groupId, newCriteria, cancellationToken);

        return RedirectToAction("Criteria", new { EditContext.StudyId, EditContext.GroupId, EditContext.FlowMode, type });
    }

    [HttpGet]
    public async Task<IActionResult> Criteria(
        int studyId, int groupId, VsiGroupCriteronType type,
        CancellationToken cancellationToken)
    {
        var group = await vipRepository.GetGroup(studyId, groupId, 
                i => new VsiGroupModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Criteria = i.Criteria.Where(c => c.Type == type).Select(c => new VsiGroupCriteriaModel
                    {
                        Id = c.Id,
                        Description = c.Description,
                        Type = c.Type
                    }).ToList()
                }, cancellationToken
            );

        return View(new CriteriaListModel{VsiGroup = group, Type = type});
    }

    [HttpGet]
    public IActionResult QueryAddExclusionCriteria()
    {
        return View();
    }

    [HttpPost]
    public IActionResult QueryAddExclusionCriteria(int groupId, [FromForm] bool addExclusionCriteria)
    {
        if (addExclusionCriteria)
        {
            return RedirectToAction("CreateCriterion", new { EditContext.StudyId, EditContext.FlowMode, groupId, type = VsiGroupCriteronType.Exclude });
        }
        else
        {
            return RedirectToAction("Section1_Step4", "VolunteerInformationPage", EditContext );
        }
    }
    

    [HttpPost]
    public async Task<IActionResult> RemoveGroup(int groupId, CancellationToken cancellationToken)
    {
        if (!await vipRepository.RemoveGroup(EditContext.StudyId, groupId, cancellationToken))
        {
            return NotFound();
        }

        return RedirectToAction("Section1_Step4", "VolunteerInformationPage", EditContext);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveCriteria(
        [FromQuery] int criteriaId,
        VsiGroupCriteronType type,
        CancellationToken cancellationToken)
    {
        bool result = await vipRepository.RemoveCriteria(EditContext.StudyId, EditContext.GroupId, criteriaId, cancellationToken);
        if (!result)
        {
            return NotFound();
        }
        
        return RedirectToAction("Criteria", new { EditContext.StudyId, EditContext.GroupId, EditContext.FlowMode, type });
    }
    
    [HttpGet]
    public async Task<IActionResult> CreateGroupCheck(CancellationToken cancellationToken)
    {
        var group = await vipRepository.GetGroup(EditContext.StudyId, EditContext.GroupId, i => new VsiGroupModel()
                {
                    Id = i.Id,
                    Name = i.Name
                }, cancellationToken
            );

        if (group == null)
        {
            return NotFound();
        }
        
        return View(group);
    }
}