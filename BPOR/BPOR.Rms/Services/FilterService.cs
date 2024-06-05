using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms.Services;

public class FilterService(ParticipantDbContext context, TimeProvider timeProvider) : IFilterService
{
    private readonly List<Expression<Func<Participant, bool>>> _filters = [];
    private readonly DateOnly _today = DateOnly.FromDateTime(timeProvider.GetLocalNow().Date);

    // too tightly coupled to view
    public IQueryable<Participant> FilterVolunteers(VolunteerFilterViewModel model, CoordinatesModel? location)
    {
        _filters.Clear();
        FilterVolunteersContacted(model.StudyId, model.SelectedVolunteersContacted);
        FilterVolunteersRegisteredInterest(model.StudyId, model.SelectedVolunteersRegisteredInterest);
        FilterVolunteersRecruited(model.StudyId, model.SelectedVolunteersRecruited);
        FilterVolunteersCompletedRegistration(model.SelectedVolunteersCompletedRegistration);
        FilterByAreasOfResearch(model.SelectedAreasOfInterest, model.IncludeNoAreasOfInterest);
        FilterByRegistrationDate(model.RegistrationFromDate.ToDateOnly(), model.RegistrationToDate.ToDateOnly());
        FilterByAge(model.AgeFrom, model.AgeTo);
        FilterBySexRegisteredAtBirth(model.IsSexMale, model.IsSexFemale,
            model.IsGenderSameAsSexRegisteredAtBirth_Yes, model.IsGenderSameAsSexRegisteredAtBirth_No,
            model.IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay);
        FilterByEthnicity(model.Ethnicity_Asian, model.Ethnicity_Black, model.Ethnicity_Mixed,
            model.Ethnicity_Other, model.Ethnicity_White);

        var query = context.Participants.AsQueryable();

        if (!string.IsNullOrWhiteSpace(model.PostcodeDistricts))
        {
            var postcodeList = model.PostcodeDistricts.Split(',', StringSplitOptions.RemoveEmptyEntries).Where(p => p is not null).Select(p => p.Trim()).ToArray();

            query = query.Where(StartsWithAnyPostCodeDistrictExpression(postcodeList));
        }

        if (model.FullPostcode != null && model.SearchRadiusMiles is not null && model.SearchRadiusMiles > 0 && location is not null)
        {
            query = FilterByRadius(location, model.SearchRadiusMiles.Value);
        }

        return _filters.Aggregate(query, (current, filter) => current.Where(filter));
    }

    private static Expression<Func<Participant, bool>> StartsWithAnyPostCodeDistrictExpression(string[] postCodeDistricts)
    {
        var expressions = postCodeDistricts
        .Select(s => (Expression<Func<Participant, bool>>)(p =>
            EF.Functions.Like(p.Address.Postcode, $"{s}%") &&
            (p.Address.Postcode == s || p.Address.Postcode.StartsWith(s + " "))
        ))
        .ToList();

        if (expressions.Count == 1) return expressions[0];

        var orExpression = expressions.Skip(2).Aggregate(
            Expression.OrElse(expressions[0].Body, Expression.Invoke(expressions[1], expressions[0].Parameters[0])),
            (x, y) => Expression.OrElse(x, Expression.Invoke(y, expressions[0].Parameters[0])));

        return Expression.Lambda<Func<Participant, bool>>(orExpression, expressions[0].Parameters);
    }

    private IQueryable<Participant> FilterByRadius(CoordinatesModel location, double radiusInMiles)
    {

        var point = new Point(location.Longitude, location.Latitude) { SRID = 4326 };

        var distanceInMeters = radiusInMiles * 1609.344;
        var boundingBox = point.Buffer(distanceInMeters / 111320).Envelope;

        return context.ParticipantLocation
            .Where(x => x.Location.Within(boundingBox) && x.Location.IsWithinDistance(point, distanceInMeters))
            .Select(pl => pl.Participant);
    }

    private void FilterByAge(int? youngestAge, int? oldestAge)
    {
        if (youngestAge.HasValue || oldestAge.HasValue)
        {
            var dobRange = _today.GetDatesWithinYearRange(youngestAge, oldestAge);

            _filters.Add(p =>
               (!dobRange.From.HasValue || dobRange.From.Value.ToDateTime(TimeOnly.MinValue) <= p.DateOfBirth) 
            && (!dobRange.To.HasValue || p.DateOfBirth <= dobRange.To.Value.ToDateTime(TimeOnly.MaxValue))
            );
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

    private void FilterVolunteersContacted(int? studyId, bool? selectedVolunteersContacted)
    {
        if (selectedVolunteersContacted == true)
        {
            Expression<Func<Participant, bool>> filterExpression = participant =>
            context.EmailCampaignParticipants
                .Any(ecp => ecp.ParticipantId == participant.Id &&
                       context.EmailCampaigns
                              .Any(ec => ec.Id == ecp.EmailCampaignId &&
                                         context.FilterCriterias
                                                .Any(fc => fc.Id == ec.FilterCriteriaId &&
                                                           fc.StudyId == studyId)));
            _filters.Add(filterExpression);
        }

        if (selectedVolunteersContacted == false)
        {
            Expression<Func<Participant, bool>> filterExpression = participant =>
            !context.EmailCampaignParticipants
                .Any(ecp => ecp.ParticipantId == participant.Id &&
                       context.EmailCampaigns
                              .Any(ec => ec.Id == ecp.EmailCampaignId &&
                                         context.FilterCriterias
                                                .Any(fc => fc.Id == ec.FilterCriteriaId &&
                                                           fc.StudyId == studyId)));

            _filters.Add(filterExpression);
        }
    }

    private void FilterVolunteersRegisteredInterest(int? studyId, bool? selectedVolunteersRegisteredInterest)
    {
        if (selectedVolunteersRegisteredInterest == true)
        {
            Expression<Func<Participant, bool>> filterExpression = participant =>
            context.EmailCampaignParticipants
                .Any(ecp => ecp.ParticipantId == participant.Id &&
                            ecp.RegisteredInterestAt != null &&
                            context.EmailCampaigns
                                   .Any(ec => ec.Id == ecp.EmailCampaignId &&
                                              context.FilterCriterias
                                                     .Any(fc => fc.Id == ec.FilterCriteriaId &&
                                                                fc.StudyId == studyId)));
            _filters.Add(filterExpression);
        }

        if (selectedVolunteersRegisteredInterest == false)
        {
            Expression<Func<Participant, bool>> filterExpression = participant =>
            !context.EmailCampaignParticipants
                .Any(ecp => ecp.ParticipantId == participant.Id &&
                    ecp.RegisteredInterestAt != null &&
                       context.EmailCampaigns
                              .Any(ec => ec.Id == ecp.EmailCampaignId &&
                                         context.FilterCriterias
                                                .Any(fc => fc.Id == ec.FilterCriteriaId &&
                                                           fc.StudyId == studyId)));
            _filters.Add(filterExpression);
        }
    }

    private void FilterVolunteersRecruited(int? studyId, bool? selectedVolunteersRecruited)
    {
        if (selectedVolunteersRecruited == true)
        {
            Expression<Func<Participant, bool>> filterExpression = participant =>
                        context.StudyParticipantEnrollment.Any(enrollment =>
                            enrollment.ParticipantId == participant.Id && enrollment.StudyId == studyId && enrollment.EnrolledAt != null);
            _filters.Add(filterExpression);
        }

        if (selectedVolunteersRecruited == false)
        {
            Expression<Func<Participant, bool>> filterExpression = participant =>
                        !context.StudyParticipantEnrollment.Any(enrollment =>
                            enrollment.ParticipantId == participant.Id && enrollment.StudyId == studyId && enrollment.EnrolledAt != null);
            _filters.Add(filterExpression);
        }
    }

    private void FilterVolunteersCompletedRegistration(bool? selectedCompletedRegistration)
    {
        if (selectedCompletedRegistration == true)
        {
            _filters.Add(p => p.Stage2CompleteUtc != null);
        }

        if (selectedCompletedRegistration == false)
        {
            _filters.Add(p => p.Stage2CompleteUtc == null);
        }
    }

    private void FilterByAreasOfResearch(List<int> selectedAreasOfInterest, bool includeNoAreasOfInterest)
    {
        if (selectedAreasOfInterest.Count != 0 || includeNoAreasOfInterest)
        {
            _filters.Add(p => p.HealthConditions.Any(hc => selectedAreasOfInterest.Contains(hc.HealthConditionId)) || (includeNoAreasOfInterest && !p.HealthConditions.Any()));
        }
    }

    private void FilterByRegistrationDate(DateOnly? registrationFromDate, DateOnly? registrationToDate)
    {
        if (registrationFromDate.HasValue || registrationToDate.HasValue)
        {
            _filters.Add(p =>
                (!registrationFromDate.HasValue || p.RegistrationConsentAtUtc >= registrationFromDate.Value.ToDateTime(TimeOnly.MinValue)) &&
                (!registrationToDate.HasValue || p.RegistrationConsentAtUtc <= registrationToDate.Value.ToDateTime(TimeOnly.MaxValue)));
        }
    }
}
