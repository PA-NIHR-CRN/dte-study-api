using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[Route("Study/{studyId:int}/VolunteerInformation/[action]")]
[Authorize(Roles = "Admin")]
public class VolunteerInformationStartController(IVipRepository vipRepository) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Start(int studyId, CancellationToken cancellationToken)
    {
        var currentVsi = (await vipRepository.GetPage(studyId, cancellationToken));

        return View(new StartModel { Status = currentVsi?.Status, StudyId = studyId });
    }

    [HttpPost]
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

        var currentVsi = (await vipRepository.GetPage(studyId, cancellationToken));
        if (currentVsi != null)
        {
            // TODO: there is an existing active draft ... Cope with this better!!
            return BadRequest();
        }

        await vipRepository.CreatePage(study, VsiStatus.Draft, cancellationToken);

        return RedirectToAction("Section1_Step1", "VolunteerInformationPage", new { studyId = studyId, flowMode = VipFlowMode.Create });
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSample(int studyId, CancellationToken cancellationToken)
    {
        await vipRepository.CreateSampleVolunteerInformation(studyId, cancellationToken);
        return RedirectToAction("Section4", "VolunteerInformationPage",  new { studyId, flowMode = VipFlowMode.Create });
    }
}