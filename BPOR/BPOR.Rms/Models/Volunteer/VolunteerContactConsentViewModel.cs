using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Volunteer
{
    public class VolunteerContactConsentViewModel
    {
        [MustBeTrue(ErrorMessage = "Confirm that the Privacy and Data Sharing Policy has been read and understood before giving consent")]
        public bool AgreedToContactConsent { get; set; }
    }

    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool boolValue && boolValue;
        }
    }
}
