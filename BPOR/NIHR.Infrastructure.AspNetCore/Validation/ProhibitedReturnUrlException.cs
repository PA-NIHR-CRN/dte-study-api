namespace NIHR.Infrastructure.AspNetCore.Validation;

public class ProhibitedRedirectUrlException(string message) : Exception(message);