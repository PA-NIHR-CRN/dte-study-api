using NIHR.GovUk.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using NIHR.Infrastructure.Models;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using Rbec.Postcodes;
using System.ComponentModel;
using BPOR.Domain.Enums;
using Amazon.Runtime.Internal.Transform;

namespace BPOR.Rms.Models.Volunteer;

public class VolunteerFormViewModel : IValidatableObject
{

    [Display(Name = "First name", Order = 1)]
    public string? FirstName { get; set; }

    [Display(Name = "Last name", Order = 2)]
    public string? LastName { get; set; }

    [Display(Name = "Date of birth", Order = 3, Description = "For example, 31 3 1980.\n\nMust be 18 or over")]
    public GovUkDate? DateOfBirth { get; set; }

    [Display(Name = "Postcode", Order = 9)]
    public Postcode? PostCode { get; set; }

    [Display(Name = "Select an address")]
    public string? SelectedAddress { get; set; }

    [Display(Name = "Address line 1", Order = 4)]
    public string? AddressLine1 { get; set; }

    [Display(Name = "Address line 2 (optional)", Order = 6)]
    public string? AddressLine2 { get; set; }

    [Display(Name = "Address line 3 (optional)", Order = 7)]
    public string? AddressLine3 { get; set; }

    [Display(Name = "Address line 4 (optional)", Order = 8)]
    public string? AddressLine4 { get; set; }

    [Display(Name = "Town", Order = 5)]
    public string? Town { get; set; }

    [Display(Name = "Preferred contact method", Order = 10)]
    public int? PreferredContactMethod { get; set; }

    [Display(Name = "Email address", Order = 11)]
    public string? EmailAddress { get; set; }


    [Display(Name = "Landline number", Order = 12)]
    public string? LandLine { get; set; }


    [Display(Name = "Mobile number", Order = 13)]
    public string? Mobile { get; set; }


    [Display(Name = "Sex registered at birth", Order = 14)]
    public int? SexRegisteredAtBirth { get; set; }

    [Display(Name = "Gender identity same as sex registered at birth", Order = 15)]
    public string? GenderIdentitySameAsBirth { get; set; }

    [Display(Name = "Ethnic group", Order = 16)]
    public string? EthnicGroup { get; set; }

    [Display(Name = "Ethnic background", Order = 17)]
    public string? EthnicBackground { get; set; }

    [Display(Name = "How would you describe your background?", Order = 17)]
    public string? EthnicBackgroundOther { get; set; }

    [Display(Name = "Long-term conditions or illnesses and reduced ability to carry out daily activities", Order = 18)]
    public int? LongTermConditionOrIllness { get; set; }

    [Display(Name = "Areas of research (optional)", Order = 19)]
    public List<int>? AreasOfResearch { get; set; }

    public bool ManualAddressEntry { get; set; }
    public string? lastAction { get; set; }

    public List<ContactMethods> GetPrefferdContactMethodValues{
        get { 
        return Enum.GetValues<ContactMethods>().ToList();
        }

    }

public List<Dictionary<string, string>> SexRegisteredAtBirthValues 
    {
        get
        {
            return Constants.SexRegisteredAtBirth.getSexRegisteredAtBirthOptions();
        }
    }
    public List<Dictionary<string, string>> EthnicGroupValues
    {
        get
        {
            return Constants.EthnicGroups.getEthnicGroupOptions();
        }
    }

    public List<Dictionary<string, string>> EthnicBackgroundOptions()
    {
        return Constants.EthnicBackgrounds.getEthnicBackground(EthnicGroup);
    }



    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (String.IsNullOrEmpty(FirstName))
        {
            yield return new ValidationResult("Enter a first name", [nameof(FirstName)]);
        }

        if (String.IsNullOrEmpty(LastName))
        {
            yield return new ValidationResult("Enter a last name", [nameof(LastName)]);
        }

        if (DateOfBirth == null || !DateOfBirth.HasAnyDateComponent)
        {
            yield return new ValidationResult($"Enter a Date of Birth", ["DateOfBirth.Day"]);
        }

        if (PostCode == null || !PostCode.HasValue)
        {
            yield return new ValidationResult("Enter a postcode", [nameof(PostCode)]);
        }

        if (PreferredContactMethod == null)
        {
            yield return new ValidationResult("Select if the preferred contact method is email or letter", [nameof(PreferredContactMethod)]);
        }

        if (SexRegisteredAtBirth == null)
        {
            yield return new ValidationResult("Select if the sex registered at birth is female or male", [nameof(SexRegisteredAtBirth)]);
        }

        if (String.IsNullOrEmpty(GenderIdentitySameAsBirth))
        {
            yield return new ValidationResult("Select gender identity same as sex registered at birth", [nameof(GenderIdentitySameAsBirth)]);
        }

        if (String.IsNullOrEmpty(EthnicGroup))
        {
            yield return new ValidationResult("Select ethnic group", [nameof(EthnicGroup)]);
        }

        if (LongTermConditionOrIllness == null)
        {
            yield return new ValidationResult("Select long-term conditions or illnesses and reduced ability to carry out daily activities", [nameof(LongTermConditionOrIllness)]);
        }

        if (String.IsNullOrEmpty(LandLine) && String.IsNullOrEmpty(Mobile))
        {
            yield return new ValidationResult( "Enter either a UK landline number or UK mobile number", [nameof(LandLine)]);
        }
        if (!String.IsNullOrEmpty(LandLine) && !Regex.IsMatch(LandLine, @"^(01|02|03)\s*\d(\s*\d)*$"))
        {
            yield return new ValidationResult("Enter a landline phone number in the correct format, like 01632 960 001", [nameof(LandLine)]);
        }
        if (!String.IsNullOrEmpty(Mobile) && !Regex.IsMatch(Mobile, @"^07\s*\d(\s*\d)*$"))
        {
            yield return new ValidationResult("Enter a mobile phone number in the correct format, like 07700 900 982", [nameof(Mobile)]);
        }
        if (PreferredContactMethod != null && PreferredContactMethod == (int)ContactMethods.Email && String.IsNullOrEmpty(EmailAddress))
        {
            yield return new ValidationResult("Email address cannot be blank", [nameof(EmailAddress)]);
        }


        if (ManualAddressEntry)
        {
            //addressline1 &town needs verified
            if (String.IsNullOrEmpty(AddressLine1))
            {
                yield return new ValidationResult( "Enter the first line of the address", [nameof(AddressLine1)]);
            }

            if (String.IsNullOrEmpty(Town))
            {
                yield return new ValidationResult( "Enter the town of the address", [nameof(Town)]);
            }
        }
        else
        {
            if (SelectedAddress != null && SelectedAddress.Contains("Addresses found"))
            {
                SelectedAddress = null;
            }
            // make sure one of the address is selected.
            if (SelectedAddress == null && PostCode.HasValue)
            {
                yield return new ValidationResult("Select an address or enter an address manually", [nameof(SelectedAddress)]);

            }
        }

        if (LongTermConditionOrIllness == 0)
        {
            yield return new ValidationResult( "Select long-term conditions or illnesses and reduced ability to carry out daily activities", [nameof(LongTermConditionOrIllness)]);
        }
        if (EthnicBackground == null && EthnicGroup != null)
        {
            yield return new ValidationResult( "Select ethnic background", [nameof(EthnicBackground)]);
        }
        // need to check for other.
        if (EthnicBackground == "Other" && EthnicBackgroundOther == null)
        {
            yield return new ValidationResult( "please describe your ethnic background", [nameof(EthnicBackgroundOther)]);
        }
        if (EthnicGroup == "Other" && String.IsNullOrEmpty(EthnicBackgroundOther))
        {
            yield return new ValidationResult( "Describe Ethnic Background", [nameof(EthnicBackgroundOther)]);
        }

        // invalid values for fields
        if (!PostCode.HasValue)
        {
            yield return new ValidationResult( "Enter a full UK postcode", [nameof(PostCode)]);
        }

        if(EmailAddress != null)
        { 
            string emailRegex = "^[^@]+@?[^@]+$";
            if (!Regex.IsMatch(EmailAddress, emailRegex))
            {
                yield return new ValidationResult( "Enter an email address in the correct format, like name@example.com", [nameof(EmailAddress)]);
            }
            if (EmailAddress.Length >= 254)
            {
                yield return new ValidationResult("The email address cannot exceed 254 characters.", [nameof(EmailAddress)]);
            }
        }

        if (DateOfBirth.HasValue)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly eighteenYearsAgo = today.AddYears(-18);
            DateOnly nineteenHundred = DateOnly.FromDateTime(new DateTime(1900, 1, 1));

            if (DateOfBirth.ToDateOnly() > eighteenYearsAgo && DateOfBirth.ToDateOnly() <= today)
            {
                yield return new ValidationResult("Volunteer must be 18 or over to use this service", ["DateOfBirth.Day"]);
            }
            if (DateOfBirth.ToDateOnly() > today)
            {
                yield return new ValidationResult( "Date of birth must be in the past", ["DateOfBirth.Month"]);
            }
            if (DateOfBirth.ToDateOnly() < nineteenHundred && DateOfBirth.Year >= 1000)
            {
                yield return new ValidationResult( "Date of birth year must be after 1900", ["DateOfBirth.Year"]);
            }
        }




    }

}

