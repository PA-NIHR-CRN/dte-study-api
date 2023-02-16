using Application.Models.ResearcherStudies;
using Dte.Study.Management.Api.Client.Responses.ResearcherStudies;

namespace Application.Mappings.Studies
{
    public static class ResearcherStudyMapper
    {
        public static ResearcherStudyModel MapTo(ResearcherStudyResponse source)
        {
            return new ResearcherStudyModel
            {
                UserId   = source.UserId,
                Firstname = source.Firstname,
                Lastname = source.Lastname,
                Email = source.Email,
                StudyId = source.StudyId,
                Role = source.Role
            };
        }
    }
}