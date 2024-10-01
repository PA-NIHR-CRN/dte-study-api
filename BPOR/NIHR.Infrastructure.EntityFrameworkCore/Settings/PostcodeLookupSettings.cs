using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHR.Infrastructure.EntityFrameworkCore.Settings
{
    public class PostcodeLookupSettings : IValidatableObject
    {
        public static string SectionName => "PostcodeLookupSettings";
        public string EndpointURL { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(EndpointURL))
            {
                yield return new ValidationResult("EndpointURL is required", new[] { nameof(EndpointURL) });
            }

            if (string.IsNullOrWhiteSpace(Username))
            {
                yield return new ValidationResult("Username is required", new[] { nameof(Username) });
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                yield return new ValidationResult("Password is required", new[] { nameof(Password) });
            }
        }

    }
}
