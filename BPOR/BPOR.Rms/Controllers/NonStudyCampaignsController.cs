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
        var nonStudyCampaignsViewModel = new NonStudyCampaignsViewModel();

        var campaignList = context.FilterCriterias
            .Where(fc => fc.StudyId == null)
            .SelectMany(fc => fc.Campaign)
            .Where(cp => cp.TypeId == ContactMethodId.Email);

        if (campaignList != null && campaignList.Any())
        {
            var pagedData = campaignList
                .Select(ec => new Models.Study.Campaign
                {
                    TargetGroupSize = (int)ec.TargetGroupSize,
                    CreatedAt = ec.CreatedAt,
                    Name = ec.Name,
                    TypeId = ec.TypeId,
                    CampaignParticipants = ec.Participant
                            .Select(p => new Models.Study.CampaignParticipant
                            {
                                SentAt = p.SentAt,
                                RegisteredInterestAt = p.RegisteredInterestAt,
                                DeliveredAt = p.DeliveredAt,
                                DeliveryStatusId = p.DeliveryStatusId
                            })
                            .ToList(),
                }).OrderByDescending(ec => ec.CreatedAt)
                .DeferredPage(paginationService);

            nonStudyCampaignsViewModel.Campaigns = pagedData.Value;
        } else
        {
            nonStudyCampaignsViewModel.Campaigns = new List<Models.Study.Campaign>();
        }

        return View(nonStudyCampaignsViewModel);
    }
}
