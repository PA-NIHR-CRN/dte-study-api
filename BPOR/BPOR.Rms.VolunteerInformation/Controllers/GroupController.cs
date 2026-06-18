using BPOR.Rms.Abstractions.Entities;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models;
using BPOR.Rms.VolunteerInformation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[AllowAnonymous]
[Route("Study/{studyId:int}/VolunteerInformation/groups/{groupId:int}/[action]")]
public class GroupController(IVipRepository vipRepository) : VipControllerBase(vipRepository)
{
    [HttpGet]
    public async Task<IActionResult> CreateCriterion(int studyId, int groupId, VsiGroupCriteronType type, 
        CancellationToken cancellationToken)
    {
        var group = await vipRepository.GetGroup(studyId, groupId,
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

        return RedirectToAction("Criteria", new { studyId, groupId, type });
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
    public IActionResult QueryAddExclusionCriteria(int studyId, int groupId)
    {
        return View();
    }

    [HttpPost]
    public IActionResult QueryAddExclusionCriteria(int studyId, int groupId, [FromForm] bool addExclusionCriteria)
    {
        if (addExclusionCriteria)
        {
            return RedirectToAction("CreateCriterion", new { studyId, groupId, type = VsiGroupCriteronType.Exclude });
        }
        else
        {
            return RedirectToAction("Section1_Step4", "VolunteerStudyInformation", new { studyId, groupId });
        }
    }
    

    [HttpPost]
    public async Task<IActionResult> RemoveGroup(int studyId, int groupId, CancellationToken cancellationToken)
    {
        if (!await vipRepository.RemoveGroup(studyId, groupId, cancellationToken))
        {
            return NotFound();
        }

        return RedirectToAction("Section1_Step4", "VolunteerStudyInformation", new { studyId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveCriteria(
        int studyId,
        int groupId,
        [FromQuery] int criteriaId,
        VsiGroupCriteronType type,
        CancellationToken cancellationToken)
    {
        bool result = await vipRepository.RemoveCriteria(studyId, groupId, criteriaId, cancellationToken);
        if (!result)
        {
            return NotFound();
        }
        
        return RedirectToAction("Criteria", new { studyId, groupId, type });
    }
    
    [HttpGet]
    public async Task<IActionResult> CreateGroupCheck(int studyId, int groupId,
        CancellationToken cancellationToken)
    {
        var group = await vipRepository.GetGroup(studyId, groupId, i => new VsiGroupModel()
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