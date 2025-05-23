using Microsoft.AspNetCore.Mvc;
using BPOR.Rms.Models;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using NIHR.Infrastructure.Paging;

namespace BPOR.Rms.Controllers;

public class NonStudyCampaignsController(
    ParticipantDbContext context,
    IPaginationService paginationService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
    {
        var campaigns = await context.FilterCriterias
            .Where(fc => fc.StudyId == null)
            .SelectMany(fc => fc.Campaign)
            .Where(cp => cp.TypeId == ContactMethodId.Email)
            .AsCampaignModel()
            .OrderByDescending(ec => ec.CreatedAt)
            .PageAsync(paginationService, cancellationToken);

        return View(campaigns);
    }
}
