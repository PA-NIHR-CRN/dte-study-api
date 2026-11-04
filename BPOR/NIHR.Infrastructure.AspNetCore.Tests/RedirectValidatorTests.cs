using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.AspNetCore.Services;
using NIHR.Infrastructure.Settings;

namespace BPOR.Rms.Tests;

public class RedirectValidatorTests
{
    [Theory]
    [InlineData("/relative/path")]
    [InlineData("../relative/path")]
    [InlineData("relative/path?query=value")]
    [InlineData("#section")]
    public void Validate_AllowsRelativeUrlsWhenConfigured(string redirectUri)
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowRelativeUrls = true
        });

        var result = validator.Validate(redirectUri);

        Assert.Null(result);
    }

    [Fact]
    public void Validate_RejectsRelativeUrlsWhenNotConfigured()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowRelativeUrls = false
        });

        var result = validator.Validate("/relative/path");

        Assert.Equal("Relative URLs are not permitted", result?.ErrorMessage);
    }

    [Fact]
    public void Validate_RejectsMalformedUri()
    {
        var validator = CreateValidator(new RedirectValidationSettings());

        var result = validator.Validate("http://[invalid");

        Assert.Equal("URL is not well-formed", result?.ErrorMessage);
    }

    [Fact]
    public void Validate_AllowsAbsoluteUrlMatchingCurrentOrigin()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = true
        });

        var result = validator.Validate("https://app.example.com/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_MatchesCurrentOriginCaseInsensitively()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = true
        });

        var result = validator.Validate("HTTPS://APP.EXAMPLE.COM/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_RejectsCurrentHostWithDifferentScheme()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = true
        });

        var result = validator.Validate("http://app.example.com/return");

        Assert.Equal("Origin not permitted", result?.ErrorMessage);
    }

    [Fact]
    public void Validate_RejectsCurrentOriginWhenNotAllowed()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false
        });

        var result = validator.Validate("https://app.example.com/return");

        Assert.Equal("Origin not permitted", result?.ErrorMessage);
    }

    [Fact]
    public void Validate_AllowsCurrentOriginWithConfiguredNonDefaultPort()
    {
        var validator = CreateValidator(
            new RedirectValidationSettings { AllowCurrentAuthority = true },
            requestPort: 8443);

        var result = validator.Validate("https://app.example.com:8443/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_AllowsOriginMatchingConfiguredAuthority()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["https"]
                }
            ]
        });

        var result = validator.Validate("https://trusted.example.com/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_MatchesConfiguredSchemeAndAuthorityCaseInsensitively()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["HTTPS"]
                }
            ]
        });

        var result = validator.Validate("https://TRUSTED.EXAMPLE.COM/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_UsesDefaultHttpsPortWhenOriginPortIsNotSpecified()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["https"]
                }
            ]
        });

        var result = validator.Validate("https://trusted.example.com:443/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_UsesDefaultHttpPortWhenOriginPortIsNotSpecified()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["http"]
                }
            ]
        });

        var result = validator.Validate("http://trusted.example.com/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_AllowsConfiguredNonDefaultPort()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["https"],
                    Port = 8443
                }
            ]
        });

        var result = validator.Validate("https://trusted.example.com:8443/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_RejectsConfiguredOriginOnDifferentPort()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["https"],
                    Port = 8443
                }
            ]
        });

        var result = validator.Validate("https://trusted.example.com:9443/return");

        Assert.Equal("Origin not permitted", result?.ErrorMessage);
    }

    [Fact]
    public void Validate_AllowsSubdomainWhenConfigured()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["https"],
                    AllowSubAuthorities = true
                }
            ]
        });

        var result = validator.Validate("https://api.trusted.example.com/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_AllowsNestedSubdomainWhenConfigured()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["https"],
                    AllowSubAuthorities = true
                }
            ]
        });

        var result = validator.Validate("https://api.eu.trusted.example.com/return");

        Assert.Null(result);
    }

    [Fact]
    public void Validate_RejectsSubdomainWhenNotConfigured()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["https"],
                    AllowSubAuthorities = false
                }
            ]
        });

        var result = validator.Validate("https://api.trusted.example.com/return");

        Assert.Equal("Origin not permitted", result?.ErrorMessage);
    }

    [Fact]
    public void Validate_RejectsHostThatOnlyEndsWithConfiguredAuthorityText()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["https"],
                    AllowSubAuthorities = true
                }
            ]
        });

        var result = validator.Validate("https://nottrusted.example.com/return");

        Assert.Equal("Origin not permitted", result?.ErrorMessage);
    }

    [Fact]
    public void Validate_RejectsConfiguredAuthorityWithUnlistedScheme()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false,
            AllowOrigins =
            [
                new OriginSpec
                {
                    Authority = "trusted.example.com",
                    Schemes = ["https"]
                }
            ]
        });

        var result = validator.Validate("http://trusted.example.com/return");

        Assert.Equal("Origin not permitted", result?.ErrorMessage);
    }

    [Fact]
    public void Validate_RejectsAbsoluteOriginNotInAllowList()
    {
        var validator = CreateValidator(new RedirectValidationSettings
        {
            AllowCurrentAuthority = false
        });

        var result = validator.Validate("https://untrusted.example.com/return");

        Assert.Equal("Origin not permitted", result?.ErrorMessage);
    }

    private static RedirectValidator CreateValidator(
        RedirectValidationSettings settings,
        string requestScheme = "https",
        string requestHost = "app.example.com",
        int? requestPort = null)
    {
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                Scheme = requestScheme,
                Host = requestPort.HasValue
                    ? new HostString(requestHost, requestPort.Value)
                    : new HostString(requestHost)
            }
        };

        return new RedirectValidator(
            new HttpContextAccessor { HttpContext = httpContext },
            Options.Create(settings));
    }
}