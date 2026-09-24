using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Enums;
using BPOR.Rms.Models.PreScreenerEligibility;
using BPOR.Rms.Models.Volunteer;

namespace BPOR.Rms.Models.Study;

public class StudyDetailsViewModel
{
    public StudyModel Study { get; set; }
    [Display(Name = "Is this study recruiting identifiable participants?\t")]
    [StudyEdit(2)]
    public string IsRecruitingIdentifiableParticipantsDisplay => Study.IsRecruitingIdentifiableParticipants ? "Yes" : "No";

    public IEnumerable<EnrollmentDetails> EnrollmentDetails { get; set; }
    public int TotalRecruited => EnrollmentDetails.Sum(e => e.RecruitmentTotal);
    public int LatestRecruitmentTotal => EnrollmentDetails.FirstOrDefault()?.RecruitmentTotal ?? 0;
    public bool HasCampaigns { get; set; } = false;
    public IEnumerable<Campaign> Campaigns { get; set; }
    public int TotalNotificationsSent => Campaigns.Sum(e => e.TotalCampaignNotificationsSent);
    public int TotalRegisteredInterest => Campaigns.Sum(e => e.TotalCampaignRegisteredInterest);
    public IEnumerable<ResearcherEmail> ResearcherEmails { get; set; }
    public List<ActionLink> ActionLinks { get; set; } = [];

    public PreScreenerEligibilityViewModel PreScreenerViewModel =>
        new()
        {
            IsEligibilityCriteriaComplete = Study.IsEligibilityCriteriaComplete,
            IsEligibleForPrescreener = Study.IsEligibleForPrescreener
        };

    public StudyStatusViewModel StudyStatusViewModel =>
        new()
        {
            StudyStatusDisplay = Study.StudyStatusDisplayWithReasons,
            StudyId = Study.Id
        };
}

public class Campaign
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? TargetGroupSize { get; set; }
    public IEnumerable<CampaignParticipant> CampaignParticipants { get; set; }
    public int TotalCampaignNotificationsSent => CampaignParticipants.Count(e => e.DeliveryStatusId == 3);
    public int TotalContactAttemptsFailed => CampaignParticipants.Count(e => e.DeliveryStatusId == 5);
    public int TotalCampaignRegisteredInterest => CampaignParticipants.Count(p => p.RegisteredInterestAt != null);
    public ContactMethodId TypeId { get; set; }

}

public class CampaignParticipant
{
    public string ContactEmail { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? RegisteredInterestAt { get; set; }
    public int? DeliveryStatusId { get; set; }
}

public class ResearcherEmail
{
    public string ResearcherEmailAddress { get; set; }
    public string? EmailTemplate { get; set; }
    public DateTime SentAt { get; set; }
    public int? DeliveryStatusId { get; set; }
    public int StudyId { get; set; }
    public int EmailTemplateId { get; set; }
}