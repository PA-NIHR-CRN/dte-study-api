using Amazon.Runtime.Internal.Transform;
using BPOR.Rms.Models.Researcher;
using NIHR.GovUk.AspNetCore.Mvc;
using Rbec.Postcodes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace BPOR.Rms.Models.Volunteer;

public class VolunteerFormViewModel : FormWithSteps
{
// should all be made nullable and control be used in the controller?

    [Display(Name = "First name", Order = 1)]
    [Required(ErrorMessage = "Enter a first name")]
    public string FirstName { get; set; }

    [Display(Name = "Last name", Order = 2)]
    [Required(ErrorMessage = "Enter a last name")]
    public string LastName { get; set; }

    [Display(Name = "Date of birth", Order = 3)]
    public GovUkDate DateOfBirth { get; set; } = new GovUkDate(1900, 2100);

    [Display(Name = "Postcode", Order = 4)]
    public Postcode? PostCode { get; set; }

    [Display(Name = "Address line 1", Order = 5)]
    [Required(ErrorMessage = "Enter the first line of the address")]
    public string AddressLine1 { get; set; }

    [Display(Name = "Address line 2 (optional)", Order = 6)]
    public string? AddressLine2 { get; set; }

    [Display(Name = "Address line 3 (optional)", Order = 7)]
    public string? AddressLine3 { get; set; }

    [Display(Name = "Address line 4 (optional)", Order = 8)]
    public string? AddressLine4 { get; set; }

    [Display(Name = "Town", Order = 9)]
    [Required(ErrorMessage = "Enter the town of the address")]
    public string Town { get; set; }

    [Display(Name = "Preferred contact method", Order = 10)]
    [Required(ErrorMessage = "Select if the preferred contact method is email or letter")]
    public string PreferredContactMethod { get; set; }

    [Display(Name = "Email address", Order = 11)]
    [StringLength(254, ErrorMessage = "The email address cannot exceed 254 characters.")]
    [EmailAddress(ErrorMessage = "Enter an email address in the correct format, like name@example.com")]
    public string? Email { get; set; }


    [Display(Name = "Landline number", Order = 12)]
    [RegularExpression(@"^(01|02|03)\s*\d(\s*\d)*$", ErrorMessage = "Enter a phone number in the correct format, like 01632 960 001")]
    public string? LandLine { get; set; }


    [Display(Name = "Mobile phone number", Order = 13)]
    [RegularExpression(@"^07\s*\d(\s*\d)*$", ErrorMessage = "Enter a phone number in the correct format, like 07700 900 982")]
    public string? Mobile { get; set; }


    [Display(Name = "Sex registered at birth", Order = 14)]
    [Required(ErrorMessage = "Select if the sex registered at birth is female or male")]
    public string SexRegisteredAtBirth { get; set; }

    [Display(Name = "Gender identity same as sex registered at birth", Order = 15)]
    [Required(ErrorMessage = "Select gender identity same as sex registered at birth")]
    public string GenderIdentitySameAsBirth { get; set; }

    [Display(Name = "Ethnic group", Order = 16)]
    [Required(ErrorMessage = "Select ethnic group")]
    public string EthnicGroup { get; set; }

    [Display(Name = "Ethnic background", Order = 17)]
    [Required(ErrorMessage = "Select ethnic background")]
    public string EthnicBackground { get; set; }

    [Display(Name = "Long-term conditions or illnesses", Order = 18)]
    [Required(ErrorMessage = "Select long-term conditions or illnesses")]
    public string LongTermConditionOrIllness { get; set; }

    [Display(Name = "Areas of research (optional)", Order = 19)]
    public List<string>? AreasOfResearch { get; set; }

    public bool IncludeNoAreasOfInterest { get; set; }



    public override int TotalSteps => 1;

    public override String StepName
    {
        get
        {
            return "Add a volunteer to the volunteer registry";
        }
    }

    // options for each form input, should be replaced by options from db.
    public List<Dictionary<string, string>> PrefferdContactMethodValues
    {
        get
        {
            var prefferdContactValues = new List<Dictionary<string, string>>();
            prefferdContactValues.Add(new Dictionary<string, string> { { "label", "Email" }, { "value", "Email" } });
            prefferdContactValues.Add(new Dictionary<string, string> { { "label", "Letter" }, { "value", "Letter" } });

            return prefferdContactValues;
        }
    }

    public List<Dictionary<string, string>> SexRegisteredAtBirthValues
    {
        get
        {
            var SexRegisteredAtBirthValues = new List<Dictionary<string, string>>();
            SexRegisteredAtBirthValues.Add(new Dictionary<string, string> { { "label", "Male" }, { "value", "Male" } });
            SexRegisteredAtBirthValues.Add(new Dictionary<string, string> { { "label", "Female" }, { "value", "Female" } });

            return SexRegisteredAtBirthValues;
        }
    }


    public List<Dictionary<string, string>> EthnicGroupValues
    {
        get
        {
            var EthnicGroupValues = new List<Dictionary<string, string>>();
            EthnicGroupValues.Add(new Dictionary<string, string> { { "label", "Asian or Asian British" }, { "value", "Asian or Asian British" } });
            EthnicGroupValues.Add(new Dictionary<string, string> { { "label", "Black, African, Black British or Caribbean" }, { "value", "Black, African, Black British or Caribbean" } });
            EthnicGroupValues.Add(new Dictionary<string, string> { { "label", "Mixed or multiple ethnic groups" }, { "value", "Mixed or multiple ethnic groups" } });
            EthnicGroupValues.Add(new Dictionary<string, string> { { "label", "White" }, { "value", "White" } });
            EthnicGroupValues.Add(new Dictionary<string, string> { { "label", "Other ethnic group" }, { "value", "Other ethnic group" } });

            return EthnicGroupValues;
        }
    }

    
    //Asian or Asian British
    public List<Dictionary<string, string>> EthnicbackgroundValuesAAB
    {
        get
        {

            var EthnicbackgroundValuesAAB = new List<Dictionary<string, string>>();

            

            EthnicbackgroundValuesAAB.Add(new Dictionary<string, string> { { "label", "Bangladeshi" }, { "value", "Bangladeshi" } });
            EthnicbackgroundValuesAAB.Add(new Dictionary<string, string> { { "label", "Chinese" }, { "value", "Chinese" } });
            EthnicbackgroundValuesAAB.Add(new Dictionary<string, string> { { "label", "Indian" }, { "value", "Indian" } });
            EthnicbackgroundValuesAAB.Add(new Dictionary<string, string> { { "label", "Pakistani" }, { "value", "Pakistani" } });
            EthnicbackgroundValuesAAB.Add(new Dictionary<string, string> { { "label", "Another Asian or Asian British background" }, { "value", "Another Asian or Asian British background" } });

            return EthnicbackgroundValuesAAB;

        }
    }

    //Black, African, Black British or Caribbean
    public List<Dictionary<string, string>> EthnicbackgroundValuesBABBC
    {
        get
        {

            var EthnicbackgroundValuesBABBC = new List<Dictionary<string, string>>();



            EthnicbackgroundValuesBABBC.Add(new Dictionary<string, string> { { "label", "African" }, { "value", "African" } });
            EthnicbackgroundValuesBABBC.Add(new Dictionary<string, string> { { "label", "Black British" }, { "value", "Black British" } });
            EthnicbackgroundValuesBABBC.Add(new Dictionary<string, string> { { "label", "Caribbean" }, { "value", "Caribbean" } });
            EthnicbackgroundValuesBABBC.Add(new Dictionary<string, string> { { "label", "Another Black, African, Black British or Caribbean background" }, { "value", "Another Black, African, Black British or Caribbean background" } });

            return EthnicbackgroundValuesBABBC;

        }
    }

    //Mixed or multiple ethnic groups
    public List<Dictionary<string, string>> EthnicbackgroundValuesMM
    {
        get
        {

            var EthnicbackgroundValuesMM = new List<Dictionary<string, string>>();



            EthnicbackgroundValuesMM.Add(new Dictionary<string, string> { { "label", "Asian and White" }, { "value", "Asian and White" } });
            EthnicbackgroundValuesMM.Add(new Dictionary<string, string> { { "label", "Black African and White" }, { "value", "Black African and White" } });
            EthnicbackgroundValuesMM.Add(new Dictionary<string, string> { { "label", "Black Caribbean and White" }, { "value", "Black Caribbean and White" } });
            EthnicbackgroundValuesMM.Add(new Dictionary<string, string> { { "label", "Another mixed background" }, { "value", "Another mixed background" } });

            return EthnicbackgroundValuesMM;

        }
    }

    //White
    public List<Dictionary<string, string>> EthnicbackgroundValuesW
    {
        get
        {

            var EthnicbackgroundValuesW = new List<Dictionary<string, string>>();



            EthnicbackgroundValuesW.Add(new Dictionary<string, string> { { "label", "British, English, Northern Irish, Scottish, or Welsh" }, { "value", "British, English, Northern Irish, Scottish, or Welsh" } });
            EthnicbackgroundValuesW.Add(new Dictionary<string, string> { { "label", "Irish" }, { "value", "Irish" } });
            EthnicbackgroundValuesW.Add(new Dictionary<string, string> { { "label", "Irish Traveller" }, { "value", "Irish Traveller" } });
            EthnicbackgroundValuesW.Add(new Dictionary<string, string> { { "label", "Roma" }, { "value", "Roma" } });
            EthnicbackgroundValuesW.Add(new Dictionary<string, string> { { "label", "Another White background" }, { "value", "Another White background" } });

            return EthnicbackgroundValuesW;

        }
    }

    //Other ethnic group
    public List<Dictionary<string, string>> EthnicbackgroundValuesO
    {
        get
        {

            var EthnicbackgroundValuesO = new List<Dictionary<string, string>>();



            EthnicbackgroundValuesO.Add(new Dictionary<string, string> { { "label", "Arab" }, { "value", "Arab" } });
            EthnicbackgroundValuesO.Add(new Dictionary<string, string> { { "label", "Any other ethnic group" }, { "value", "Any other ethnic group" } });

            return EthnicbackgroundValuesO;

        }
    }



}

