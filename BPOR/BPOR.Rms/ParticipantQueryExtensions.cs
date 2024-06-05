using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Domain.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms;

public static class ParticipantQueryExtensions
{
    public static IQueryable<Participant> WhereVolunteersContacted(this IQueryable<Participant> query, int? studyId, bool? selectedVolunteersContacted)
    {
        if (selectedVolunteersContacted == true)
        {
            return query.Where(x => x.EmailCampaignParticipants.Any(e => e.EmailCampaign.FilterCriteria.StudyId == studyId));
        }

        if (selectedVolunteersContacted == false)
        {
            return query.Where(x => !x.EmailCampaignParticipants.Any(e => e.EmailCampaign.FilterCriteria.StudyId == studyId));
        }

        return query;
    }

    public static IQueryable<Participant> WhereVolunteersRegisteredInterest(this IQueryable<Participant> query, int? studyId, bool? selectedVolunteersRegisteredInterest)
    {
        if (selectedVolunteersRegisteredInterest == true)
        {
            return query.Where(x => x.EmailCampaignParticipants.Any(e => e.RegisteredInterestAt != null && e.EmailCampaign.FilterCriteria.StudyId == studyId));
        }

        if (selectedVolunteersRegisteredInterest == false)
        {
            return query.Where(x => x.EmailCampaignParticipants.Any(e => e.RegisteredInterestAt == null && e.EmailCampaign.FilterCriteria.StudyId == studyId));
        }

        return query;
    }

    public static IQueryable<Participant> WhereVolunteersRecruited(this IQueryable<Participant> query, int? studyId, bool? selectedVolunteersRecruited)
    {
        if (selectedVolunteersRecruited == true)
        {
            return query.Where(x => x.StudyParticipantEnrollments.Any(e => e.StudyId == studyId && e.EnrolledAt != null));
        }

        if (selectedVolunteersRecruited == false)
        {
            return query.Where(x => x.StudyParticipantEnrollments.Any(e => e.StudyId == studyId && e.EnrolledAt == null));
        }

        return query;
    }

    public static IQueryable<Participant> WhereVolunteersCompletedRegistration(this IQueryable<Participant> query, bool? selectedCompletedRegistration)
    {
        if (selectedCompletedRegistration == true)
        {
            return query.Where(x => x.Stage2CompleteUtc != null);
        }

        if (selectedCompletedRegistration == false)
        {
            return query.Where(x => x.Stage2CompleteUtc == null);
        }

        return query;
    }

    public static IQueryable<Participant> WhereHasAnyAreaOfResearch(this IQueryable<Participant> query, IEnumerable<int> selectedAreasOfInterest, bool includeNoAreasOfInterest)
    {
        if (selectedAreasOfInterest.Any() || includeNoAreasOfInterest)
        {
            return query.Where(p => p.HealthConditions.Any(hc => selectedAreasOfInterest.Contains(hc.HealthConditionId)) || includeNoAreasOfInterest && !p.HealthConditions.Any());
        }

        return query;
    }

    public static IQueryable<Participant> WhereHasRegistrationDateInRange(this IQueryable<Participant> query, DateOnly? registrationFromDate, DateOnly? registrationToDate)
    {
        if (registrationFromDate.HasValue || registrationToDate.HasValue)
        {
            return query.Where(p =>
                (!registrationFromDate.HasValue || p.RegistrationConsentAtUtc >= registrationFromDate.Value.ToDateTime(TimeOnly.MinValue)) &&
                (!registrationToDate.HasValue || p.RegistrationConsentAtUtc <= registrationToDate.Value.ToDateTime(TimeOnly.MaxValue)));
        }

        return query;
    }

    public static IQueryable<Participant> WhereHasAgeInRange(this IQueryable<Participant> query, TimeProvider timeProvider, int? youngestAge, int? oldestAge)
    {
        DateOnly _today = DateOnly.FromDateTime(timeProvider.GetLocalNow().Date);

        if (youngestAge.HasValue || oldestAge.HasValue)
        {
            var dobRange = _today.GetDatesWithinYearRange(youngestAge, oldestAge);

            return query.Where(p =>
               (!dobRange.From.HasValue || dobRange.From.Value.ToDateTime(TimeOnly.MinValue) <= p.DateOfBirth)
            && (!dobRange.To.HasValue || p.DateOfBirth <= dobRange.To.Value.ToDateTime(TimeOnly.MaxValue))
            );
        }

        return query;
    }

    public static IQueryable<Participant> WhereHasSexRegisteredAtBirth(this IQueryable<Participant> query, bool isSexMale, bool isSexFemale)
    {
        if (isSexMale || isSexFemale)
        {
            return query.Where(p => isSexMale && p.GenderId == (int)GenderId.Male || isSexFemale && p.GenderId == (int)GenderId.Female);
        }

        return query;
    }

    public static IQueryable<Participant> WhereHasGenderSameAsSexRegisteredAtBirth(this IQueryable<Participant> query,
            bool isGenderSameAsSexRegisteredAtBirthYes, bool isGenderSameAsSexRegisteredAtBirthNo,
            bool isGenderSameAsSexRegisteredAtBirthPreferNotToSay)
    {
        if (isGenderSameAsSexRegisteredAtBirthYes || isGenderSameAsSexRegisteredAtBirthNo ||
            isGenderSameAsSexRegisteredAtBirthPreferNotToSay)
        {
            return query.Where(p =>
                isGenderSameAsSexRegisteredAtBirthYes && p.GenderIsSameAsSexRegisteredAtBirth == true ||
                isGenderSameAsSexRegisteredAtBirthNo && p.GenderIsSameAsSexRegisteredAtBirth == false ||
                isGenderSameAsSexRegisteredAtBirthPreferNotToSay && p.GenderIsSameAsSexRegisteredAtBirth == null);
        }

        return query;
    }

    public static IQueryable<Participant> WhereHasEthnicity(this IQueryable<Participant> query, bool ethnicityAsian, bool ethnicityBlack, bool ethnicityMixed,
    bool ethnicityOther, bool ethnicityWhite)
    {
        if (ethnicityAsian || ethnicityBlack || ethnicityMixed || ethnicityOther || ethnicityWhite)
        {
            return query.Where(p =>
                ethnicityAsian && p.EthnicGroup == "asian" ||
                ethnicityBlack && p.EthnicGroup ==  "black" ||
                ethnicityMixed && p.EthnicGroup ==  "mixed" ||
                ethnicityOther && p.EthnicGroup ==  "other" ||
                ethnicityWhite && p.EthnicGroup ==  "white");
        }

        return query;
    }

    public static IQueryable<Participant> WhereWithinRadiusOfLocation(this IQueryable<Participant> query, CoordinatesModel? location, double? radiusInMiles)
    {
        if (location is not null && radiusInMiles is not null && radiusInMiles > 0)
        {
            {
                var point = new Point(location.Longitude, location.Latitude) { SRID = 4326 };

                double distanceInMeters = radiusInMiles.Value * 1609.344;
                var boundingBox = point.Buffer(distanceInMeters / 111320).Envelope;

                return query.Where(x => x.ParticipantLocation.Location.Within(boundingBox) && x.ParticipantLocation.Location.IsWithinDistance(point, distanceInMeters));
            }
        }

        return query;
    }

    public static IQueryable<Participant> WhereStartsWithAnyPostcodeDistrict(this IQueryable<Participant> query, IEnumerable<string> postCodeDistricts)
    {
        if (postCodeDistricts.Any())
        {
            var expression = postCodeDistricts
            .Select(s => (Expression<Func<Participant, bool>>)(p =>
                EF.Functions.Like(p.Address.Postcode, $"{s}%"))
            )
            .Aggregate((x, y) => x.OrElse(y));

            return query.Where(expression);
        }

        return query;
    }
}
