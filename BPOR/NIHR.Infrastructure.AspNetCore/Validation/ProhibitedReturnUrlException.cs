namespace NIHR.Infrastructure.AspNetCore.Validation;

public class ProhibitedRedirectUriException(string message) : Exception(message);