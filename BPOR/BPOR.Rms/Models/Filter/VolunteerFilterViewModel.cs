using BPOR.Domain.Entities.Configuration;
using NIHR.Infrastructure.Paging;
using Rbec.Postcodes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
    public List<int> SelectedAreasOfInterest { get; set; } = [];

    public bool IncludeNoAreasOfInterest { get; set; }

    [Display(Name = "Date of volunteer registration (from)")]
    public GovUkDate RegistrationFromDate { get; set; } = new();

    [Display(Name = "Date of volunteer registration (to)")]
    public GovUkDate RegistrationToDate { get; set; } = new();

    // Postcode districts and Full postcode
    [Display(Name = "Postcode districts")]
    public string? PostcodeDistricts { get; set; }

    [Display(Name = "Full postcode")]
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

    public VolunteerFilterViewTestingModel Testing { get; set; } = new();

    public ISet<string> GetPostcodeDistricts() => PostcodeDistricts?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet() ?? [];

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

        if(IsGenderSameAsSexRegisteredAtBirth_Yes)
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
        var validationChain = ValidateRegistrationDates(validationContext)
                        .Concat(ValidatePostcodeDistricts(validationContext))
                        .Concat(ValidateFullPostcode(validationContext))
                        .Concat(ValidateAge(validationContext));

        foreach (var error in validationChain)
        {
            yield return error;
        }
    }

    private IEnumerable<ValidationResult> ValidateRegistrationDates(ValidationContext validationContext)
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
                yield return new ValidationResult($"{validationContext.GetMemberDisplayName(nameof(RegistrationFromDate))} date must be on or before {validationContext.GetMemberDisplayName(nameof(RegistrationToDate))}");
            }
        }
    }

    private IEnumerable<ValidationResult> ValidatePostcodeDistricts(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(PostcodeDistricts) && !string.IsNullOrEmpty(FullPostcode))
        {
            yield return new ValidationResult("Postcode district search and Full postcode search cannot be applied at the same time", [nameof(PostcodeDistricts), nameof(FullPostcode)]);
        }

        if (!string.IsNullOrEmpty(PostcodeDistricts))
        {
            var postcodeDistrictsList = PostcodeDistricts.Split(",");
            string pattern = @"^[A-Za-z]{1,2}[0-9]{1,2}[A-Za-z]?$";

            foreach (var item in postcodeDistrictsList)
            {
                if (!Regex.IsMatch(item.Trim(), pattern))
                {
                    yield return new ValidationResult("Enter a UK postcode district in the correct format, like PO15 or LS1", [nameof(PostcodeDistricts)]);
                }
            }
        }
    }

    private IEnumerable<ValidationResult> ValidateFullPostcode(ValidationContext validationContext)
    {

        if (!string.IsNullOrEmpty(FullPostcode))
        {
            if (!Postcode.TryParse(FullPostcode, out Postcode postcode))
            {
                yield return new ValidationResult(
                        "Enter a full UK postcode", [nameof(FullPostcode)]);
            }
        }

        if (!string.IsNullOrEmpty(FullPostcode) && SearchRadiusMiles == null)
        {
            yield return new ValidationResult(
                        "Enter a radius", [nameof(SearchRadiusMiles)]);
        }

        if (string.IsNullOrEmpty(FullPostcode) && SearchRadiusMiles != null)
        {
            yield return new ValidationResult(
                        "Enter a postcode", [nameof(FullPostcode)]);
        }

        if (!string.IsNullOrEmpty(FullPostcode) && SearchRadiusMiles == 0)
        {
            yield return new ValidationResult(
                        $"{validationContext.GetMemberDisplayName(nameof(SearchRadiusMiles))} must be greater than 0", [nameof(SearchRadiusMiles)]);
        }
    }

    private IEnumerable<ValidationResult> ValidateAge(ValidationContext validationContext)
    {
        if (AgeFrom > AgeTo)
        {
            yield return new ValidationResult($"Age {validationContext.GetMemberDisplayName(nameof(AgeFrom))} must be less than or equal to the Age {validationContext.GetMemberDisplayName(nameof(AgeTo))}", [nameof(AgeFrom)]);
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

public class GovUkDate : IValidatableObject
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

    public bool HasValue => Day.HasValue && Month.HasValue && Year.HasValue;

    public bool HasAnyDateComponent => Day.HasValue || Month.HasValue || Year.HasValue;

    public DateOnly? ToDateOnly()
    {
        if (!HasValue)
        {
            return null;
        }

        try
        {
            return new DateOnly(Year.GetValueOrDefault(), Month.GetValueOrDefault(), Day.GetValueOrDefault());
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }

    public static GovUkDate FromDateTime(DateTime? date) => new GovUkDate { Day = date?.Day, Month = date?.Month, Year = date?.Year };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!HasAnyDateComponent) { yield break; }

        if (!Day.HasValue)
        {
            yield return new ValidationResult($"{validationContext.GetMemberDisplayName(nameof(Day))} must include a day.", [nameof(Day)]);
        }

        if (!Month.HasValue)
        {
            yield return new ValidationResult($"{validationContext.GetMemberDisplayName(nameof(Month))} must include a month.", [nameof(Month)]);
        }

        if (!Year.HasValue)
        {
            yield return new ValidationResult($"{validationContext.GetMemberDisplayName(nameof(Year))} must include a year.", [nameof(Year)]);
        }
    }
}

public class VolunteerFilterViewTestingModel
{
    public Page<VolunteerResult> VolunteerResults { get; set; } = Page<VolunteerResult>.Empty();

    public bool ShowResults { get; set; }
}