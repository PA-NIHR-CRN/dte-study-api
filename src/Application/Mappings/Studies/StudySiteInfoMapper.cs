using Application.Responses.V1.Studies;
using StudySiteResponse = Dte.Study.Management.Api.Client.Responses.Studies.StudySiteResponse;

namespace Application.Mappings.Studies
{
    public static class StudySiteInfoMapper
    {
        public static StudySiteInfoResponse MapTo(StudySiteResponse source)
        {
            return new StudySiteInfoResponse
            {
                StudyId = source.StudyId,
                StudySiteStatus = source.StudySiteStatus,
                Name = source.Name,
                Identifier = source.Identifier,
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                AddressLine3 = source.AddressLine3,
                AddressLine4 = source.AddressLine4,
                AddressLine5 = source.AddressLine5,
                Postcode = source.Postcode
            };
        }
    }
}