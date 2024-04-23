using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Entities;

namespace BPOR.Rms.Models.Email;

public class SetupCampaignViewModel
{
    public int? StudyId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a number greater than zero for Max Numbers.")]
    public int MaxNumbers { get; set; }

    [DisplayName("How many volunteers do you want to send it to?")]
    [Range(1, int.MaxValue, ErrorMessage = "Total Volunteers must be at least 1.")]
    public int TotalVolunteers { get; set; }

    public string? StudyName { get; set; }

    [Required(ErrorMessage = "Please select a email template.")]
    public string? SelectedTemplate { get; set; }

    [DisplayName("Preview email")]
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string? PreviewEmails { get; set; }

    public NotificationBannerModel? Notification { get; set; }
    public bool? Contacted { get; set; }
    public bool? RegisteredInterest { get; set; }
    public bool? CompletedRegistration { get; set; }
    public bool? Recruited { get; set; }
    public ICollection<ParticipantHealthCondition>? HealthConditions { get; set; }
    public ICollection<string>? PostcodeDistricts { get; set; }
    public string? FullPostcode { get; set; }
    public decimal? SearchRadiusMiles { get; set; }
    public DateTime? RegistrationFromDate { get; set; }
    public DateTime? RegistrationToDate { get; set; }
    public DateTime? DateOfBirthFrom { get; set; }
    public DateTime? DateOfBirthTo { get; set; }
    public int? GenderId { get; set; }
    public bool? GenderIsSameAsSexRegisteredAtBirth { get; set; }
    [MaxLength(255)] public string? EthnicGroup { get; set; }
}
