﻿using BPOR.Domain.Entities.RefData;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Researcher
{
    public class ResearcherStudyFormViewModel : FormWithSteps
    {
        public override int TotalSteps => 8;

        [Display(Name = "Study ID")]
        public int Id { get; set; }

        [Display(Name = "What is the study short name?", Order = 1)]
        public string? ShortName { get; set; }

        [Display(Name = "Who is the Chief Investigator for the study?", Order = 2)]
        public string? ChiefInvestigator { get; set; }

        [Display(Name = "Provide the name(s) of the study sponsor(s), funder(s) and CRO (if applicable)", Order = 3)]
        public string? StudySponsors { get; set; }

        public List<Submitted>? PortfolioSubmissionStatusOptions { get; set; }

        [Display(Name = "Has the study been submitted for inclusion on the NIHR CRN portfolio?", Order = 4)]
        public int? PortfolioSubmissionStatus { get; set; }

        public List<SubmissionOutcome>? OutcomeOfSubmissionOptions { get; set; }

        [Display(Name = "Outcome of submission", Order = 5)]
        public int? OutcomeOfSubmission { get; set; }        

        [Display(Name = "CPMS ID", Order = 6)]
        public long? CPMSId { get; set; }

        [Display(Name = "Does the study have NIHR funding?", Order = 7)]
        public bool? HasFunding { get; set; }

        [Display(Name = "NIHR funding stream or grant code", Order = 8)]
        public string? FundingCode { get; set; }

        [Display(Name = "What is the UK recruitment target for the study?", Order = 9)]
        public string? UKRecruitmentTarget { get; set; }

        [Display(Name = "What is the target population for the study?", Order = 10)]
        public string? TargetPopulation { get; set; }
        [Display(Order = 11)]
        public GovUkDate RecruitmentStartDate { get; set; } = new();
        [Display(Order = 12)]
        public GovUkDate RecruitmentEndDate { get; set; } = new();

        [Display(Name = "Will participants in the study be recruited as named individual volunteers?", Order = 13)]
        public bool? RecruitingIdentifiableVolunteers { get; set; }
    }

    public class GovUkDate
    {

        [Display(Name = "Day", Order = 1)]
        [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
        public int? Day { get; set; }

        [Display(Name = "Month", Order = 2)]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        public int? Month { get; set; }

        [Display(Name = "Year", Order = 3)]
        public int? Year { get; set; }


        private static DateOnly? ConstructDate(int? year, int? month, int? day)
        {
            if (!year.HasValue || !month.HasValue || !day.HasValue)
            {
                return null;
            }

            try
            {
                return new DateOnly(year.Value, month.Value, day.Value);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        public DateOnly? ToDateOnly() => ConstructDate(Year, Month, Day);

        public static GovUkDate FromDateTime(DateTime? date) => new GovUkDate { Day = date?.Day, Month = date?.Month, Year = date?.Year };
    }
}
