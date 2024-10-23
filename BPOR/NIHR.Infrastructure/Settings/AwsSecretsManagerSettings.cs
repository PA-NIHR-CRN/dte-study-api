using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NIHR.Infrastructure.Settings
{
    public class AwsSecretsManagerSettings : IValidatableObject
    {
        public static string SectionName => "AwsSecretsManagerSettings";
        public bool Enabled { get; set; } = true;
        public string Region { get; set; } = Environment.GetEnvironmentVariable("AwsSecretsManagerSettings__Region");
        public string SecretName { get; set; } = Environment.GetEnvironmentVariable("AwsSecretsManagerSettings__SecretName");


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Enabled)
            {
                if (string.IsNullOrWhiteSpace(Region))
                {
                    yield return new ValidationResult("Region is required", new[] { nameof(Region) });
                }

                if (string.IsNullOrWhiteSpace(SecretName))
                {
                    yield return new ValidationResult("SecretName is required", new[] { nameof(SecretName) });
                }
            }
        }
    }
}
