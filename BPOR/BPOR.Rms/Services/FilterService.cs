using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure;

namespace BPOR.Rms.Services;

public class FilterService(ParticipantDbContext context, IPostcodeMapper locationApiClient) : IFilterService
{
    private readonly List<Expression<Func<Participant, bool>>> _filters = [];

    public async Task<IQueryable<Participant>> FilterVolunteersAsync(VolunteerFilterViewModel model, CancellationToken cancellationToken = default)
    {
        _filters.Clear();
        FilterVolunteersCompletedRegistration(model.SelectedVolunteersCompletedRegistration);
        FilterByAreasOfResearch(model.SelectedHealthConditions);
        FilterByRegistrationDate(model.RegistrationFromDateDay, model.RegistrationFromDateMonth,
            model.RegistrationFromDateYear,
            model.RegistrationToDateDay, model.RegistrationToDateMonth, model.RegistrationToDateYear);
        FilterByAge(model.AgeFrom, model.AgeTo);
        FilterBySexRegisteredAtBirth(model.IsSexMale, model.IsSexFemale,
            model.IsGenderSameAsSexRegisteredAtBirth_Yes, model.IsGenderSameAsSexRegisteredAtBirth_No,
            model.IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay);
        FilterByEthnicity(model.Ethnicity_Asian, model.Ethnicity_Black, model.Ethnicity_Mixed,
            model.Ethnicity_Other, model.Ethnicity_White);

        var query = context.Participants.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(model.PostcodeDistricts))
        {
            var postcodeList = model.PostcodeDistricts.Split(',').Select(p => p.Trim()).ToList();
            query = FilterByPostcodePrefix(postcodeList);
        }

        if (model.FullPostcode != null && model.SearchRadiusMiles > 0)
        {
            var radiusInMeters = (double)(model.SearchRadiusMiles * (decimal?)1609.34); 
            query = await FilterByRadius(model.FullPostcode, radiusInMeters, cancellationToken);
        }

        return _filters.Aggregate(query, (current, filter) => current.Where(filter));
    }

    private IQueryable<Participant> FilterByPostcodePrefix(List<string> postcodeList)
    {
        var query = context.ParticipantAddress.Where(pa => false);

        foreach (var prefix in postcodeList)
        {
            var localPrefix = prefix; 
            query = context.ParticipantAddress
                .Where(pa =>  EF.Functions.Like(pa.Postcode, $"{localPrefix}%"))
                .Concat(query);
        }

        return query.Distinct().Select(pa => pa.Participant);
    }

    private async Task<IQueryable<Participant>> FilterByRadius(string postcode, double radiusInMeters, CancellationToken cancellationToken = default)
    {
        var coordinates = await locationApiClient.GetCoordinatesFromPostcodeAsync(postcode, cancellationToken);

        var point = new Point(coordinates.Latitude, coordinates.Longitude) { SRID = 4326 };
        return context.ParticipantLocation
            .Where(pl => pl.Location.IsWithinDistance(point, radiusInMeters))
            .Select(pl => pl.Participant);
    }

    private void FilterByAge(int? ageFrom, int? ageTo)
    {
        if (ageFrom != null || ageTo != null)
        {
            DateTime fromDate = ageTo.HasValue ? DateTime.Today.AddYears(-ageTo.Value) : DateTime.MinValue;
            DateTime toDate = ageFrom.HasValue ? DateTime.Today.AddYears(-ageFrom.Value) : DateTime.MaxValue;

            _filters.Add(p => p.DateOfBirth >= fromDate && p.DateOfBirth <= toDate);
        }
    }

    private void FilterBySexRegisteredAtBirth(bool isSexMale, bool isSexFemale,
        bool isGenderSameAsSexRegisteredAtBirthYes, bool isGenderSameAsSexRegisteredAtBirthNo,
        bool isGenderSameAsSexRegisteredAtBirthPreferNotToSay)
    {
        if (isSexMale || isSexFemale)
        {
            if (!isSexMale)
            {
                _filters.Add(p => p.GenderId == 2);
            }
            else if (!isSexFemale)
            {
                _filters.Add(p => p.GenderId == 1);
            }
            else
            {
                _filters.Add(p => p.GenderId == 1 || p.GenderId == 2);
            }
        }

        if (isGenderSameAsSexRegisteredAtBirthYes || isGenderSameAsSexRegisteredAtBirthNo ||
            isGenderSameAsSexRegisteredAtBirthPreferNotToSay)
        {
            _filters.Add(p =>
                (isGenderSameAsSexRegisteredAtBirthYes && p.GenderIsSameAsSexRegisteredAtBirth == true) ||
                (isGenderSameAsSexRegisteredAtBirthNo && p.GenderIsSameAsSexRegisteredAtBirth == false) ||
                (isGenderSameAsSexRegisteredAtBirthPreferNotToSay && p.GenderIsSameAsSexRegisteredAtBirth == null));
        }
    }

    private void FilterByEthnicity(bool ethnicityAsian, bool ethnicityBlack, bool ethnicityMixed,
        bool ethnicityOther, bool ethnicityWhite)
    {
        if (ethnicityAsian || ethnicityBlack || ethnicityMixed || ethnicityOther || ethnicityWhite)
        {
            _filters.Add(p =>
                (ethnicityAsian && p.EthnicGroup.ToLower() == "asian") ||
                (ethnicityBlack && p.EthnicGroup.ToLower() == "black") ||
                (ethnicityMixed && p.EthnicGroup.ToLower() == "mixed") ||
                (ethnicityOther && p.EthnicGroup.ToLower() == "other") ||
                (ethnicityWhite && p.EthnicGroup.ToLower() == "white"));
        }
    }

    private void FilterVolunteersCompletedRegistration(string? selectedCompletedRegistration)
    {
        if (selectedCompletedRegistration == "1")
        {
            _filters.Add(p => p.Stage2CompleteUtc != null);
        }

        if (selectedCompletedRegistration == "2")
        {
            _filters.Add(p => p.Stage2CompleteUtc == null);
        }
    }

    private void FilterByAreasOfResearch(List<string>? selectedHealthConditions)
    {
        if (selectedHealthConditions != null)
        {
            List<int> conditionIds = selectedHealthConditions.Select(s => int.Parse(s)).ToList();

            _filters.Add(p => p.HealthConditions.Any(hc => conditionIds.Contains(hc.HealthConditionId)));
        }
    }

    private void FilterByRegistrationDate(int? registrationFromDateDay, int? registrationFromDateMonth,
        int? registrationFromDateYear,
        int? registrationToDateDay, int? registrationToDateMonth, int? registrationToDateYear)
    {
        if ((registrationFromDateDay != null && registrationFromDateMonth != null &&
             registrationFromDateYear != null) ||
            (registrationToDateDay != null && registrationToDateMonth != null && registrationToDateYear != null))
        {
            if (registrationToDateDay.HasValue)
            {
                registrationToDateDay++;
            }

            DateTime? registrationFromDate = ConstructDate(registrationFromDateYear, registrationFromDateMonth,
                registrationFromDateDay);
            DateTime? registrationToDate =
                ConstructDate(registrationToDateYear, registrationToDateMonth, registrationToDateDay);

            _filters.Add(p =>
                (!registrationFromDate.HasValue || p.RegistrationConsentAtUtc >= registrationFromDate) &&
                (!registrationToDate.HasValue || p.RegistrationConsentAtUtc <= registrationToDate));
        }
    }


    //TODO move to shared
    private static DateTime? ConstructDate(int? year, int? month, int? day)
    {
        if (!year.HasValue || !month.HasValue || !day.HasValue)
            return null;

        try
        {
            return new DateTime(year.Value, month.Value, day.Value);
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }
}
