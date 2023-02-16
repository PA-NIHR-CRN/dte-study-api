using System;

namespace Application.Responses.V1.Studies
{
    public class StudySiteResponse
    {
        public long StudyId { get; set; }
        public string StudySiteStatus { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Type { get; set; }
        public string ParentOrganisation { get; set; }
        public DateTime? EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string Postcode { get; set; }
        public string UkCountryIdentifier { get; set; }
        public string UkCountryName { get; set; }
    }
}