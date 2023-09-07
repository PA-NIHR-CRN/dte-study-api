using System;

namespace Infrastructure.Exceptions;

public class CognitoPhoneNumberUpdateException : Exception
{
    public CognitoPhoneNumberUpdateException(string message) : base(message)
    {
    }
}