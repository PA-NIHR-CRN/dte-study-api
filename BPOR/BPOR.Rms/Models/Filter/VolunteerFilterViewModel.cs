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
    public bool ExcludeEnrolled { get; set; }
    public bool ExcludeCompletedRegistration { get; set; }
    public bool IncludeContacted { get; set; }
    public bool IncludeRegisteredInterest { get; set; }
    public bool IncludeCompletedRegistration { get; set; }
    public bool IncludeEnrolled { get; set; }

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
    public bool? IsGenderSameAsSexRegisteredAtBirth { get; set; }
    public IEnumerable<string> SelectedEthnicGroups { get; set; }
    public IEnumerable<SelectListItem> EthnicGroups { get; set; }

    // Additional properties as needed for other form fields
}

// Supporting classes for dropdowns
public class SelectListItem
{
    public string Text { get; set; }
    public string Value { get; set; }
    public bool Selected { get; set; }
}
