using BPOR.Domain.Entities.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure;
using Rbec.Postcodes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BPOR.Rms.Models.Filter
{
    public class PostcodeSearchModel : IValidatableObject
    {
        [Display(Name = "Postcode districts", Description = "Enter one or more postcode districts separated by commas.\r\nFor example: SW1A, WC2B, EC1V.")]
        public string? PostcodeDistricts { get; set; }

        public PostcodeRadiusSearchModel PostcodeRadiusSearch { get; set; } = new();

        public ISet<string> GetPostcodeDistricts() => PostcodeDistricts?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet() ?? [];


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(PostcodeDistricts) && PostcodeRadiusSearch.HasValue())
            {
                yield return new ValidationResult("Postcode district search and Full postcode search cannot be applied at the same time", [nameof(PostcodeDistricts)]);
            }

            if (!string.IsNullOrEmpty(PostcodeDistricts))
            {
                var postcodeDistrictsList = PostcodeDistricts.Split(",");
                string pattern = @"^[A-Za-z]{1,2}[0-9]{1,2}[A-Za-z]?$";

                foreach (var item in postcodeDistrictsList)
                {
                    if (!Regex.IsMatch(item.Trim(), pattern))
                    {
                        yield return new ValidationResult("Enter a UK postcode district in the correct format, like PO15 or LS1", [nameof(PostcodeDistricts)]);
                    }
                }
            }
        }
    }

    public class PostcodeRadiusSearchModel : IValidatableObject
    {
        [Display(Name = "Full postcode", Description = "For example: EC1A 1BB.")]

        public Postcode? FullPostcode { get; set; }

        [LookupLocation(From = nameof(FullPostcode))]
        public Point? Location { get; set; }

        [Display(Name = "Radius", Description = "Indicate the radius for searching volunteers based on the provided full postcode.")]
        [IntegerOrDecimal(ErrorMessage = "Enter a whole number or a positive number with one decimal place, like 8 or 1.3", RequiredIfNotNull = nameof(FullPostcode))]
        public double? SearchRadiusMiles { get; set; }

        public bool HasValue() => FullPostcode.HasValue && SearchRadiusMiles.HasValue;

        public double? GetRadiusInMetres() => SearchRadiusMiles * 1609.344;
        public double? GetRadiusInDegrees() => GetRadiusInMetres() / 111320;

        public Geometry? GetBoundingBox()
        {
            var radiusInDegress = GetRadiusInDegrees();
            if (radiusInDegress == null || Location is null)
            {
                return null;
            }

            return Location.Buffer(radiusInDegress.Value).Envelope;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (FullPostcode.HasValue && SearchRadiusMiles is null)
            {
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateProperty(SearchRadiusMiles, validationContext, results))
                {
                    foreach (var item in results)
                    {
                        yield return new ValidationResult(item.ErrorMessage, [nameof(SearchRadiusMiles)]);
                    }
                }
            }

            if (!FullPostcode.HasValue && SearchRadiusMiles != null)
            {
                yield return new ValidationResult(
                            "Enter a postcode", [nameof(FullPostcode)]);
            }

            if (FullPostcode.HasValue && SearchRadiusMiles <= 0)
            {
                yield return new ValidationResult(
                            $"{validationContext.GetMemberDisplayName(nameof(SearchRadiusMiles))} must be greater than 0", [nameof(SearchRadiusMiles)]);
            }
        }
    }

    public class LookupLocationAttribute : ModelBinderAttribute
    {
        public LookupLocationAttribute()
        {
            this.BinderType = typeof(LookupLocationModelBinder);
        }

        public string From { get; set; }
    }

    public class LookupLocationModelBinder : IModelBinder
    {
        private readonly IPostcodeMapper _postcodeMapper;

        public LookupLocationModelBinder(IPostcodeMapper postcodeMapper)
        {
            _postcodeMapper = postcodeMapper;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelMetadata is DefaultModelMetadata x)
            {
                var attribute = x.Attributes.PropertyAttributes.Select(z => z as LookupLocationAttribute).Where(z => z is not null).First();

                var modelName = bindingContext.ModelName.Split('.')[..^1].Append(attribute.From);

                var s = bindingContext.ValueProvider.GetValue(string.Join('.', modelName));
                var source = s.Values.First();

                if (Postcode.TryParse(source, out var postcode))
                {
                    var location = await _postcodeMapper.GetCoordinatesFromPostcodeAsync(postcode.ToString(), bindingContext.HttpContext.RequestAborted);

                    bindingContext.Result = ModelBindingResult.Success(new Point(location.Longitude, location.Latitude) { SRID = ParticipantLocationConfiguration.LocationSrid });
                }
            }
        }
    }
}