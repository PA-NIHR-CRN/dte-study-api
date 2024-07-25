using BPOR.Domain.Entities.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using NIHR.Infrastructure.Paging;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Filter;

public class VolunteerFilterViewModel : IValidatableObject
{
    public int? StudyId { get; set; }
    // Study selection
    [Display(Name = "Selected Study")]
    public string? StudyName { get; set; }

    public long? StudyCpmsId { get; set; }

    public bool HasStudy() => StudyId is not null;

    public bool ShowRecruitedFilter { get; set; }

    [Display(Name = "Volunteers contacted", Order = 1)]
    public bool? SelectedVolunteersContacted { get; set; }
    public IEnumerable<SelectListItem> VolunteersContactedItems { get; set; } = SetVolunteersContactedItems();

    private static IEnumerable<SelectListItem> SetVolunteersContactedItems()
    {
        var items = new List<SelectListItem>
        {
            new SelectListItem { Value = string.Empty, Text = "No preference" },
            new SelectListItem { Value = true.ToString(), Text = "Contacted" },
            new SelectListItem { Value = false.ToString(), Text = "Not contacted" }
        }; 
           
        return items;
    }

    [Display(Name = "Volunteers recruited", Order = 3)]
    public bool? SelectedVolunteersRecruited { get; set; }
    public IEnumerable<SelectListItem> VolunteersRecruitedItems { get; set; } = SetVolunteersRecruitedItems();

    private static IEnumerable<SelectListItem> SetVolunteersRecruitedItems()
    {
        var items = new List<SelectListItem>
        {
            new SelectListItem { Value = string.Empty, Text = "No preference" },
            new SelectListItem { Value = true.ToString(), Text = "Recruited" },
            new SelectListItem { Value = false.ToString(), Text = "Not recruited" }
        };

        return items;
    }

    [Display(Name = "Volunteers registered interest", Order = 2)]
    public bool? SelectedVolunteersRegisteredInterest { get; set; }
    public IEnumerable<SelectListItem> VolunteersRegisteredInterestItems { get; set; } = SetVolunteersRegisteredInterestItems();

    private static IEnumerable<SelectListItem> SetVolunteersRegisteredInterestItems()
    {
        var items = new List<SelectListItem>
        {
            new SelectListItem { Value = string.Empty, Text = "No preference" },
            new SelectListItem { Value = true.ToString(), Text = "Registered interest" },
            new SelectListItem { Value = false.ToString(), Text = "Not registered interest" }
        };

        return items;
    }

    [Display(Name = "Volunteers completed registration", Order = 4)]
    public bool? SelectedVolunteersCompletedRegistration { get; set; }
    public IEnumerable<SelectListItem> VolunteersCompletedRegistrationItems { get; set; } = SetVolunteersCompletedRegistrationItems();

    private static IEnumerable<SelectListItem> SetVolunteersCompletedRegistrationItems()
    {
        var items = new List<SelectListItem>
        {
            new SelectListItem { Value = string.Empty, Text = "No preference" },
            new SelectListItem { Value = true.ToString(), Text = "Completed registration" },
            new SelectListItem { Value = false.ToString(), Text = "Not completed registration" }
        };

        return items;
    }

    // Areas of research volunteers are interested in
    [Display(Name = "Areas of research volunteers are interested in")]
    public List<int> SelectedAreasOfInterest { get; set; } = [];

    public bool IncludeNoAreasOfInterest { get; set; }

    [Display(Name = "Date of volunteer registration (from)", Order = 5)]
    public GovUkDate RegistrationFromDate { get; set; } = new();

    [Display(Name = "Date of volunteer registration (to)", Order = 6)]
    public GovUkDate RegistrationToDate { get; set; } = new();

    public PostcodeSearchModel PostcodeSearch { get; set; } = new();


    // Demographic information
    [Display(Name = "Age range", Description = "Please specify the age range you wish to filter. The minimum starting age is 18.", Order = 8)]
    public AgeRange AgeRange { get; set; } = new();


    [Display(Name = "Male", Order = 9)]
    public bool IsSexMale { get; set; }

    [Display(Name = "Female", Order = 10)]
    public bool IsSexFemale { get; set; }

    [Display(Name = "Yes", Order = 11)]
    public bool IsGenderSameAsSexRegisteredAtBirth_Yes { get; set; }
    [Display(Name = "No", Order = 12)]
    public bool IsGenderSameAsSexRegisteredAtBirth_No { get; set; }
    [Display(Name = "Prefer Not To Say", Order = 13)]
    public bool IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay { get; set; }
    [Display(Name = "Asian", Order = 14)]
    public bool Ethnicity_Asian { get; set; }
    [Display(Name = "Black", Order = 15)]
    public bool Ethnicity_Black { get; set; }
    [Display(Name = "Mixed", Order = 16)]
    public bool Ethnicity_Mixed { get; set; }
    [Display(Name = "Other", Order = 17)]
    public bool Ethnicity_Other { get; set; }
    [Display(Name = "White", Order = 18)]
    public bool Ethnicity_White { get; set; }
    public int? VolunteerCount { get; set; }

    public VolunteerFilterViewTestingModel Testing { get; set; } = new();

    public ISet<GenderId?> GetGenderOptions()
    {
        var retval = new HashSet<GenderId?>();
        if (IsSexFemale)
        {
            retval.Add(GenderId.Female);
        }

        if (IsSexMale)
        {
            retval.Add(GenderId.Male);
        }

        // TODO: prefer not to say

        return retval;
    }

    public ISet<bool?> GetGenderSameAsSexRegisteredAtBirthOptions()
    {
        var retval = new HashSet<bool?>();

        if (IsGenderSameAsSexRegisteredAtBirth_Yes)
        {
            retval.Add(true);
        }

        if (IsGenderSameAsSexRegisteredAtBirth_No)
        {
            retval.Add(false);
        }

        if (IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay)
        {
            retval.Add(null);
        }

        return retval;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var timeProvider = validationContext.GetRequiredService<TimeProvider>();
        var _today = DateOnly.FromDateTime(timeProvider.GetLocalNow().Date);

        if (RegistrationFromDate.HasValue)
        {
            if (RegistrationFromDate.ToDateOnly() > _today)
            {
                yield return new ValidationResult($"{validationContext.GetMemberDisplayName(nameof(RegistrationFromDate))} must be on or before today", [$"{nameof(RegistrationFromDate)}.{nameof(RegistrationFromDate.Day)}"]);
            }

            if (RegistrationFromDate.Year < 2022)
            {
                yield return new ValidationResult($"{validationContext.GetMemberDisplayName(nameof(RegistrationFromDate))} year must be 2022 or later", [$"{nameof(RegistrationFromDate)}.{nameof(RegistrationFromDate.Year)}"]);
            }
        }

        if (RegistrationToDate.HasValue)
        {
            if (RegistrationToDate.ToDateOnly() > _today)
            {
                yield return new ValidationResult($"{validationContext.GetMemberDisplayName(nameof(RegistrationToDate))} must be on or before today", [$"{nameof(RegistrationToDate)}.{nameof(RegistrationToDate.Year)}"]);
            }

            if (RegistrationToDate.Year < 2022)
            {
                yield return new ValidationResult($"{validationContext.GetMemberDisplayName(nameof(RegistrationToDate))} year must be 2022 or later", [$"{nameof(RegistrationToDate)}.{nameof(RegistrationToDate.Year)}"]);
            }

        }

        if (RegistrationToDate.ToDateOnly().HasValue)
        {
            if (RegistrationFromDate.ToDateOnly() > RegistrationToDate.ToDateOnly())
            {
                yield return new ValidationResult($"{validationContext.GetMemberDisplayName(nameof(RegistrationFromDate))} date must be on or before {validationContext.GetMemberDisplayName(nameof(RegistrationToDate))}",
                    new[] { $"{nameof(RegistrationFromDate)}.{nameof(RegistrationFromDate.Day)}" ,$"{nameof(RegistrationToDate)}.{nameof(RegistrationToDate.Day)}"});
            }
        }
    }


    public ISet<string?> GetEthnicityOptions()
    {
        var retval = new HashSet<string?>();

        if (Ethnicity_Asian)
        {
            retval.Add("asian");
        }

        if (Ethnicity_Black)
        {
            retval.Add("black");
        }

        if (Ethnicity_Mixed)
        {
            retval.Add("mixed");
        }

        if (Ethnicity_Other)
        {
            retval.Add("other");
        }

        if (Ethnicity_White)
        {
            retval.Add("white");
        }

        return retval;
    }
}

public class AgeRange : IValidatableObject
{
    [Display(Name = "From")]
    [Range(18, int.MaxValue, ErrorMessage = "Enter a whole number equal to or more than 18")]
    public int? From { get; set; }
    [Display(Name = "To")]
    [Range(18, int.MaxValue, ErrorMessage = "Enter a whole number equal to or more than 18")]
    public int? To { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (From > To)
        {
            yield return new ValidationResult($"Age {validationContext.GetMemberDisplayName(nameof(From))} must be less than or equal to the Age {validationContext.GetMemberDisplayName(nameof(To))}", [nameof(From)]);
        }
    }
}

public class VolunteerFilterViewTestingModel
{
    public Page<VolunteerResult> VolunteerResults { get; set; } = Page<VolunteerResult>.Empty();

    public bool ShowResults { get; set; }
}
