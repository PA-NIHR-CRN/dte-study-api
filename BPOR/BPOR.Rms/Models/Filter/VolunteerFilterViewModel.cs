using NIHR.Infrastructure.Paging;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Filter;

public class VolunteerFilterViewModel
{
    public int? StudyId { get; set; }
    // Study selection
    [Display(Name = "Selected Study")]
    public string? StudyName { get; set; }

    public long? StudyCpmsId { get; set; }

    public bool HasStudy() => StudyId is not null;
    public bool ShowRecruitedFilter { get; set; }


    [Display(Name = "Volunteers contacted")]
    public bool? SelectedVolunteersContacted { get; set; }
    [Display(Name = "Volunteers recruited")]
    public bool? SelectedVolunteersRecruited { get; set; }
    [Display(Name = "Volunteers registered interest")]
    public bool? SelectedVolunteersRegisteredInterest { get; set; }
    [Display(Name = "Volunteers completed registration")]
    public bool? SelectedVolunteersCompletedRegistration { get; set; }

    // Areas of research volunteers are interested in
    [Display(Name = "Areas of research volunteers are interested in")]
    public List<int> SelectedHealthConditions { get; set; } = [];

    [Display(Name = "Only include volunteers without registered areas of interest")]
    public bool IncludeNoHealthConditions { get; set; }

    public GovUkDate RegistrationFromDate { get; set; } = new();

    public GovUkDate RegistrationToDate { get; set; } = new();

    // Postcode districts and Full postcode
    [Display(Name = "Postcode districts")]
    public string? PostcodeDistricts { get; set; }

    [Display(Name = "Full postcode")]
    [UKPostcode(ErrorMessage = "Enter a full UK postcode")]
    public string? FullPostcode { get; set; }

    [Display(Name = "Radius")]
    [IntegerOrDecimal(ErrorMessage = "Enter a whole number or a number with one decimal place, like 8 or 1.3")]
    public double? SearchRadiusMiles { get; set; }

    // Demographic information
    [Display(Name = "From")]
    [Range(18, int.MaxValue, ErrorMessage = "Enter a whole number equal to or more than 18")]
    public int? AgeFrom { get; set; }
    [Display(Name = "To")]
    [Range(18, int.MaxValue, ErrorMessage = "Enter a whole number equal to or more than 18")]
    public int? AgeTo { get; set; }

    [Display(Name = "Male")]
    public bool IsSexMale { get; set; }

    [Display(Name = "Female")]
    public bool IsSexFemale { get; set; }

    [Display(Name = "Yes")]
    public bool IsGenderSameAsSexRegisteredAtBirth_Yes { get; set; }
    [Display(Name = "No")]
    public bool IsGenderSameAsSexRegisteredAtBirth_No { get; set; }
    [Display(Name = "Prefer Not To Say")]
    public bool IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay { get; set; }
    [Display(Name = "Asian")]
    public bool Ethnicity_Asian { get; set; }
    [Display(Name = "Black")]
    public bool Ethnicity_Black { get; set; }
    [Display(Name = "Mixed")]
    public bool Ethnicity_Mixed { get; set; }
    [Display(Name = "Other")]
    public bool Ethnicity_Other { get; set; }
    [Display(Name = "White")]
    public bool Ethnicity_White { get; set; }
    public int? VolunteerCount { get; set; }

    public VolunteerFilterViewTestingModel Testing { get; set; } = new ();

}

public class GovUkDate
{

    [Display(Name = "Day")]
    [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
    public int? Day { get; set; }

    [Display(Name = "Month")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
    public int? Month { get; set; }

    [Display(Name = "Year")]
    [Range(1970, 2100, ErrorMessage = "Year must be a reasonable value")]
    public int? Year { get; set; }

    private static DateOnly? ConstructDate(int? year, int? month, int? day)
    {
        if (!year.HasValue || !month.HasValue || !day.HasValue)
            return null;

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

public class VolunteerFilterViewTestingModel
{
    public Page<VolunteerResult> VolunteerResults { get; set; } = Page<VolunteerResult>.Empty();

    public bool ShowResults { get; set; }
}