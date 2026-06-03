using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

public class IntegerOrDecimalAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return false; 

        var input = value.ToString();

        if (int.TryParse(input, out _))
        {
            return true;
        }

        ErrorMessage = "Enter the total number of Be Part of Research volunteers recruited";
        return false;
    }
}
