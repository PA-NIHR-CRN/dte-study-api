using BPOR.Domain.Entities.RefData;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Researcher
{
    public class ResearcherStudyFormViewModel
    {
        public int Step { get; set; } = 1;
        public int TotalSteps { get; set; } = 9;

        [Display(Name = "What is the study short name?")]
        public string? ShortName { get; set; }

        [Display(Name = "Who is the Chief Investigator for the study?")]
        public string? ChiefInvestigator { get; set; }

        [Display(Name = "Provide the name(s) of the study sponsor(s), funder(s) and CRO (if applicable)")]
        public string? StudySponsors { get; set; }

        public List<Submitted>? PortfolioSubmissionStatusOptions { get; set; }

        [Display(Name = "Has the study been submitted for inclusion on the NIHR CRN portfolio?")]
        public int? PortfolioSubmissionStatus { get; set; }

        public List<SubmissionOutcome>? OutcomeOfSubmissionOptions { get; set; }

        [Display(Name = "Outcome of submission")]
        public int? OutcomeOfSubmission { get; set; }

        [Display(Name = "CPMS ID")]
        public string? CPMSId { get; set; }

        [Display(Name = "Does the study have NIHR funding?")]
        public bool? HasFunding { get; set; }

        [Display(Name = "NIHR funding stream or grant code")]
        public string? FundingCode { get; set; }

        [Display(Name = "What is the UK recruitment target for the study?")]
        public string? UKRecruitmentTarget { get; set; }

        [Display(Name = "What is the target population for the study?")]
        public string? TargetPopulation { get; set; }

        [Display(Name = "Day")]
        public int? RecruitmentStartDateDay { get; set; }

        [Display(Name = "Month")]
        public int? RecruitmentStartDateMonth { get; set; }

        [Display(Name = "Year")]
        public int? RecruitmentStartDateYear { get; set; }

        [Display(Name = "Day")]
        public int? RecruitmentEndDateDay { get; set; }

        [Display(Name = "Month")]
        public int? RecruitmentEndDateMonth { get; set; }

        [Display(Name = "Year")]
        public int? RecruitmentEndDateYear { get; set; }

        [Display(Name = "Will participants in the study be recruited as named individual volunteers?")]
        public bool? RecruitingIdentifiableVolunteers { get; set; }
    }
}
