using Application.Responses.V1.Studies;

namespace Application.Mappings.Studies
{
    public static class StudySiteMapper
    {
        public static StudySiteResponse MapTo(Dte.Study.Management.Api.Client.Responses.Studies.StudySiteResponse source)
        {
            return new StudySiteResponse
            {
                Identifier = source.Identifier,
                Name = source.Name,
                StudySiteStatus = source.StudySiteStatus,
                Type = source.Type,
                StudyId = source.StudyId,
                ParentOrganisation = source.ParentOrganisation,
                CreatedDate = source.CreatedDate,
                ModifiedDate = source.ModifiedDate,
                EffectiveStartDate = source.EffectiveStartDate,
                EffectiveEndDate = source.EffectiveEndDate,
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                AddressLine3 = source.AddressLine3,
                AddressLine4 = source.AddressLine4,
                AddressLine5 = source.AddressLine5,
                Postcode = source.Postcode,
                UkCountryIdentifier = source.UkCountryIdentifier,
                UkCountryName = source.UkCountryName
            };
        }
    }
}