namespace Application.Responses.V1.Studies
{
    public class StudySiteInfoResponse
    {
        public long StudyId { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string StudySiteStatus { get; set; }
        public string AddressLine1{ get; set; }
        public string AddressLine2{ get; set; }
        public string AddressLine3{ get; set; }
        public string AddressLine4{ get; set; }
        public string AddressLine5{ get; set; }
        public string Postcode{ get; set; }
    }
}