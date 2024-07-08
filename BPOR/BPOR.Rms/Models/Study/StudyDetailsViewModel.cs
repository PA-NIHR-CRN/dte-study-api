using System.ComponentModel.DataAnnotations;
using BPOR.Rms.Models.Volunteer;

namespace BPOR.Rms.Models.Study;

public class StudyDetailsViewModel
{
    public StudyModel Study { get; set; }
    [Display(Name = "Is this study recruiting identifiable participants?\t")]
    public string IsRecruitingIdentifiableParticipantsDisplay => Study.IsRecruitingIdentifiableParticipants ? "Yes" : "No";
    [Display(Name = "Does the study have NIHR funding?")]
    public string HasFundingDisplay => Study.HasFunding.Value ? "Yes" : "No";
    public IEnumerable<EnrollmentDetails> EnrollmentDetails { get; set; }
    public int TotalRecruited => EnrollmentDetails.Sum(e => e.RecruitmentTotal);
    public int LatestRecruitmentTotal => EnrollmentDetails.FirstOrDefault()?.RecruitmentTotal ?? 0;
    public bool HasEmailCampaigns { get; set; } = false;
    public IEnumerable<EmailCampaign> EmailCampaigns { get; set; }
    public int TotalEmailsSent => EmailCampaigns.Sum(e => e.TotalCampaignEmailsSent);
    public int TotalRegisteredInterest => EmailCampaigns.Sum(e => e.TotalCampaignRegisteredInterest);
    public bool IsResearcher { get; set; } = false;

}

public class EmailCampaign
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? TargetGroupSize { get; set; }
    public IEnumerable<EmailCampaignParticipant> EmailCampaignParticipants { get; set; }
    public int TotalCampaignEmailsSent => EmailCampaignParticipants.Where(e => e.DeliveryStatusId == 3).Count();
    public int TotalCampaignRegisteredInterest => EmailCampaignParticipants.Where(p => p.RegisteredInterestAt != null).Count();
}

public class EmailCampaignParticipant
{
    public string ContactEmail { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? RegisteredInterestAt { get; set; }
    public int? DeliveryStatusId { get; set; }
}
