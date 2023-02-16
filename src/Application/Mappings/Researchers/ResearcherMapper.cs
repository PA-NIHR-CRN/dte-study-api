using Application.Responses.V1.Researchers;

namespace Application.Mappings.Researchers
{
    public static class ResearcherMapper
    {
        public static ResearcherResponse MapTo(Dte.Study.Management.Api.Client.Responses.Researchers.ResearcherResponse source)
        {
            return new ResearcherResponse
            {
                Id = source.Id,
                Email = source.Email,
                Firstname = source.Firstname,
                Lastname = source.Lastname,
            };
        }
    }
}