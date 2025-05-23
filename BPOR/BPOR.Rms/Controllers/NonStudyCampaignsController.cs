using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BPOR.Rms.Models;
using Microsoft.AspNetCore.Authorization;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Models.Email;
using BPOR.Domain.Enums;
using NIHR.Infrastructure.Paging;

namespace BPOR.Rms.Controllers;

public class NonStudyCampaignsController(
    ParticipantDbContext context,
    IPaginationService paginationService
) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var nonStudyCampaignsViewModel = new StudyDetailsViewModel
        {
            Campaigns = context.FilterCriterias
                .Where(fc => fc.StudyId == null)
                .SelectMany(fc => fc.Campaign)
                .Where(cp => cp.TypeId == ContactMethodId.Email)
                .AsCampaign()
                .OrderByDescending(ec => ec.CreatedAt)
                .DeferredPage(paginationService)
                .Value
        };

        return View(nonStudyCampaignsViewModel);
    }
}
