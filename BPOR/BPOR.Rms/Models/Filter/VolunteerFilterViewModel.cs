using BPOR.Domain.Entities;
using BPOR.Domain.Entities.RefData;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Filter;

public class VolunteerFilterViewModel
{
    // Study selection
    public string SelectedStudy { get; set; }
    public IEnumerable<SelectListItem> Studies { get; set; }

    // Volunteer criteria
    public bool ExcludeContacted { get; set; }
    public bool ExcludeRegisteredInterest { get; set; }
    public bool ExcludeCompletedRegistration { get; set; }
    public bool ExcludeRecruited { get; set; }
    public bool IncludeContacted { get; set; }
    public bool IncludeRegisteredInterest { get; set; }
    public bool IncludeCompletedRegistration { get; set; }
    public bool IncludeRecruited { get; set; }

    // Areas of research volunteers are interested in
    public string SelectedLocation { get; set; }
    public IEnumerable<SelectListItem> Locations { get; set; }

    // Date of volunteer registration
    // From Date
    [Display(Name = "Day")]
    [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
    public int? RegistrationFromDateDay { get; set; }

    [Display(Name = "Month")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
    public int? RegistrationFromDateMonth { get; set; }

    [Display(Name = "Year")]
    [Range(1900, 2100, ErrorMessage = "Year must be a reasonable value")]
    public int? RegistrationFromDateYear { get; set; }

    // To Date
    [Display(Name = "Day")]
    [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
    public int? RegistrationToDateDay { get; set; }

    [Display(Name = "Month")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
    public int? RegistrationToDateMonth { get; set; }

    [Display(Name = "Year")]
    [Range(1900, 2100, ErrorMessage = "Year must be a reasonable value")]
    public int? RegistrationToDateYear { get; set; }

    // Postcode districts and Full postcode
    public string PostcodeDistricts { get; set; }
    public string FullPostcode { get; set; }
    public int? SearchRadiusMiles { get; set; }

    // Demographic information
    public int? AgeFrom { get; set; }
    public int? AgeTo { get; set; }

    [Display(Name = "Male")]
    [Required(ErrorMessage = "At least one property must be selected.")]
    public bool IsSexMale { get; set; }

    [Display(Name = "Female")]
    [Required(ErrorMessage = "At least one property must be selected.")]
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
    public string VolunteerCount { get; set; } = "-";

}

// Supporting classes for dropdowns
public class SelectListItem
{
    public string Text { get; set; }
    public string Value { get; set; }
    public bool Selected { get; set; }
}
