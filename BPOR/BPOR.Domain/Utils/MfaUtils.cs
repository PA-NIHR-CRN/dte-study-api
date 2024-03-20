using Dte.Common.Exceptions;
using FluentValidation.Results;
using PhoneNumbers;

namespace BPOR.Domain.Utils;

public static class MfaUtils
{
    public static string CleanPhoneNumber(string phoneNumber)
    {
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

        // Ensure it's a valid UK number using libphonenumber
        var number = phoneUtil.Parse(phoneNumber, "GB");
        if (!phoneUtil.IsValidNumber(number))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("PhoneNumber", $"{number} is not a valid UK number")
            }, "PhoneNumber");
        }

        // Check if the number is specifically a mobile number
        if (phoneUtil.GetNumberType(number) != PhoneNumberType.MOBILE)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("PhoneNumber", $"{number} is not a valid UK mobile number")
            }, "PhoneNumber");
        }

        // Ensure the phone number is in the correct format for cognito i.e., +441234567890 E164
        return phoneUtil.Format(number, PhoneNumberFormat.E164);
    }

    public static string GenerateOtpCode()
    {
        var random = new Random();
        var code = random.Next(100000, 999999).ToString();
        return code;
    }
}
