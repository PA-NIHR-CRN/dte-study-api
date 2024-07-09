using NetTopologySuite.Geometries;

namespace BPOR.Rms.Models.Filter
{
    public class VolunteerResult
    {
        public int Id { get; internal set; }
        public string? Email { get; internal set; }
        public string? Postcode { get; internal set; }
        public IEnumerable<string> AreasOfResearch { get; internal set; } = Enumerable.Empty<string>();
        public DateTime? DateOfBirth { get; internal set; }
        public int? Age { get; internal set; }
        public string? Gender { get; internal set; }
        public Point? Location { get; internal set; }
        public string? FirstName { get; internal set; }
        public string? LastName { get; internal set; }
        public bool HasCompletedRegistration { get; internal set; }
        public DateTime? HasRegistered { get; internal set; }
        public string? EthnicGroup { get; internal set; }
        public bool? GenderIsSameAsSexRegisteredAtBirth { get; internal set; }
        public double? DistanceInMiles { get; internal set; }
    }
}