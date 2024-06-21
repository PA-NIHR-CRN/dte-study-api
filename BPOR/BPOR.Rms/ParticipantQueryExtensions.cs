using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Domain.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms;

public static class ParticipantQueryExtensions
{
    public static IQueryable<Participant> WhereContacted(this IQueryable<Participant> query, int? studyId, bool? selectedVolunteersContacted)
    {
        if (selectedVolunteersContacted.HasValue)
        {
            return query.Where(x => x.EmailCampaignParticipants.Any(e => e.EmailCampaign.FilterCriteria.StudyId == studyId) == selectedVolunteersContacted.Value);
        }

        return query;
    }

    public static IQueryable<Participant> WhereRegisteredInterest(this IQueryable<Participant> query, int? studyId, bool? selectedVolunteersRegisteredInterest)
    {
        if (selectedVolunteersRegisteredInterest.HasValue)
        {
            return query.Where(x => x.EmailCampaignParticipants.Any(e => e.RegisteredInterestAt.HasValue && e.EmailCampaign.FilterCriteria.StudyId == studyId) == selectedVolunteersRegisteredInterest.Value);
        }

        return query;
    }

    public static IQueryable<Participant> WhereRecruited(this IQueryable<Participant> query, int? studyId, bool? selectedVolunteersRecruited)
    {
        if (selectedVolunteersRecruited.HasValue)
        {
            return query.Where(x => x.StudyParticipantEnrollments.Any(e => e.EnrolledAt.HasValue && e.StudyId == studyId) == selectedVolunteersRecruited.Value);
        }

        return query;
    }

    public static IQueryable<Participant> WhereCompletedRegistration(this IQueryable<Participant> query, bool? selectedCompletedRegistration)
    {
        if (selectedCompletedRegistration.HasValue)
        {
            return query.Where(x => (x.Stage2CompleteUtc != null) == selectedCompletedRegistration.Value);
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

    public static IQueryable<Participant> WhereHasSexRegisteredAtBirth(this IQueryable<Participant> query, ISet<GenderId?> options)
    {
        if (options.Any())
        {
            var intOptions = options.Select(x => (int?)x);
            return query.Where(p => intOptions.Contains(p.GenderId));
        }

        return query;
    }

    public static IQueryable<Participant> WhereHasGenderSameAsSexRegisteredAtBirth(this IQueryable<Participant> query, ISet<bool?> options)
    {
        if (options.Any())
        {
            return query.Where(p => options.Contains(p.GenderIsSameAsSexRegisteredAtBirth));
        }

        return query;
    }

    public static IQueryable<Participant> WhereHasEthnicity(this IQueryable<Participant> query, ISet<string?> ethnicgroups)
    {
        if (ethnicgroups.Any())
        {
            return query.Where(p => ethnicgroups.Contains(p.EthnicGroup));
        }

        return query;
    }

    public static IQueryable<Participant> WhereWithinRadiusOfLocation(this IQueryable<Participant> query, CoordinatesModel? location, double? radiusInMiles)
    {
        if (location is not null && radiusInMiles is not null && radiusInMiles > 0)
        {
            {
                var point = new Point(location.Longitude, location.Latitude) { SRID = ParticipantLocationConfiguration.LocationSrid };

                double distanceInMeters = radiusInMiles.Value * 1609.344;
                var boundingBox = point.Buffer(distanceInMeters / 111320).Envelope;

                return query.Where(x => x.ParticipantLocation.Location.Within(boundingBox) && x.ParticipantLocation.Location.IsWithinDistance(point, distanceInMeters));
            }
        }

        return query;
    }

    public static IQueryable<Participant> WhereHasAnyPostcodeDistrict(this IQueryable<Participant> query, ISet<string> postCodeDistricts)
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
