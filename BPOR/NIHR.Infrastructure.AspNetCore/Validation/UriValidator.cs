using FluentValidation;

namespace NIHR.Infrastructure.AspNetCore.Validation;

public class UriValidator : AbstractValidator<string>
{
    const int maxUrlLength = 2048;
    
    public UriValidator(UriKind uriKind = UriKind.Absolute, params string[] schemes)
    {
        RuleFor(x => x)
            .Custom((value, context) =>
            {
                if (schemes.Length == 0)
                {
                    schemes = ["https"];
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                value = value.Trim();

                if (value.Length > maxUrlLength)
                {
                    context.AddFailure($"The link must be less than {maxUrlLength + 1} characters");
                }

                if (!Uri.TryCreate(value, uriKind, out var uri))
                {
                    context.AddFailure("The link you entered isn’t in the correct format");
                    return;
                }

                if (!schemes.Contains(uri.Scheme, StringComparer.OrdinalIgnoreCase))
                {
                    context.AddFailure(
                        $"The link you entered isn’t in the correct format - it must start with {string.Join(" or ", schemes.Select(i => $"{i}://"))}");
                }
            });

    }
}