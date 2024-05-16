using BPOR.Rms.Models.Volunteer;

namespace BPOR.Rms.Models.Study;

public class StudyDetailsViewModel
{
    public StudyModel Study { get; set; }
    public string IsRecruitingIdentifiableParticipantsDisplay => Study.IsRecruitingIdentifiableParticipants ? "Yes" : "No";
    public IEnumerable<EnrollmentDetails> EnrollmentDetails { get; set; }
    public int TotalRecruited => EnrollmentDetails.Sum(e => e.RecruitmentTotal);
    public int LatestRecruitmentTotal => EnrollmentDetails.FirstOrDefault()?.RecruitmentTotal ?? 0;
    public bool HasEmailCampaigns { get; set; } = false;
    public IEnumerable<EmailCampaign> EmailCampaigns { get; set; }


}

public class EmailCampaign
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? TargetGroupSize { get; set; }
    public IEnumerable<EmailCampaignParticipant> EmailCampaignParticipants { get; set; }
}

public class EmailCampaignParticipant
{
    public string ContactEmail { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? RegisteredInterestAt { get; set; }
}
