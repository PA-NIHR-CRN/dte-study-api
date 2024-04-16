using Amazon.DynamoDBv2.Model;
using BPOR.Domain.Entities;
using BPOR.Domain.Entities.RefData;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Filter;

public class VolunteerFilterViewModel
{
    // Study selection
    [Display(Name = "Select a study (if applicable) you want to find volunteers for")]
    public SelectListItem? SelectedStudy { get; set; }

    public IEnumerable<SelectListItem>? Studies { get; set; }
    [Display(Name = "Volunteers contacted")]
    public IEnumerable<SelectListItem>? VolunteersContacted { get; set; }

    [Display(Name = "Volunteers recruited")]
    public IEnumerable<SelectListItem>? VolunteersRecruited { get; set; }

    [Display(Name = "Volunteers registered interest")]
    public IEnumerable<SelectListItem>? VolunteersRegisteredInterest { get; set; }

    [Display(Name = "Volunteers completed registration")]
    public IEnumerable<SelectListItem>? VolunteersCompletedRegistration { get; set; }

    // Volunteer criteria
    [Display(Name = "Exclude those contacted")]
    public bool ExcludeContacted { get; set; }

    [Display(Name = "Exclude registered interest")]
    public bool ExcludeRegisteredInterest { get; set; }

    [Display(Name = "Exclude those who have completed registration")]
    public bool ExcludeCompletedRegistration { get; set; }

    [Display(Name = "Exclude recruited")]
    public bool ExcludeRecruited { get; set; }

    [Display(Name = "Only include those contacted")]
    public bool IncludeContacted { get; set; }

    [Display(Name = "Only include registered interest")]
    public bool IncludeRegisteredInterest { get; set; }

    [Display(Name = "Only include those who have completed registration")]
    public bool IncludeCompletedRegistration { get; set; }

    [Display(Name = "Only include recruited")]
    public bool IncludeRecruited { get; set; }

    // Areas of research volunteers are interested in
    [Display(Name = "Areas of research volunteers are interested in")]
    public List<string>? SelectedLocations { get; set; }
    public IEnumerable<SelectListItem>? Locations { get; set; }

    // Date of volunteer registration
    // From Date
    [Display(Name = "Day")]
    [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
    public int? RegistrationFromDateDay { get; set; }

    [Display(Name = "Month")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
    public int? RegistrationFromDateMonth { get; set; }

    [Display(Name = "Year")]
    [Range(2022, 2100, ErrorMessage = "Year must be a reasonable value")]
    public int? RegistrationFromDateYear { get; set; }

    // To Date
    [Display(Name = "Day")]
    [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
    public int? RegistrationToDateDay { get; set; }

    [Display(Name = "Month")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
    public int? RegistrationToDateMonth { get; set; }

    [Display(Name = "Year")]
    [Range(2022, 2100, ErrorMessage = "Year must be a reasonable value")]
    public int? RegistrationToDateYear { get; set; }

    // Postcode districts and Full postcode
    [Display(Name = "Postcode districts")]
    public string? PostcodeDistricts { get; set; }

    [Display(Name = "Full postcode")]
    [UKPostcode(ErrorMessage = "Enter a full UK postcode")]
    public string? FullPostcode { get; set; }

    [Display(Name = "Radius")]
    [IntegerOrDecimal(ErrorMessage = "Enter a whole number or a number with one decimal place, like 8 or 1.3")]
    public decimal? SearchRadiusMiles { get; set; }

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
    public string VolunteerCount { get; set; } = "-";

}