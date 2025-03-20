using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Researcher
{
    public class ResearcherTermsAndConditionsViewModel
    {
        [MustBeTrue(ErrorMessage = "Confirm that you have read and agree to the terms and conditions before applying")]
        public bool AgreedToTermsAndConditions { get; set; }
    }

    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool boolValue && boolValue;
        }
    }
}
