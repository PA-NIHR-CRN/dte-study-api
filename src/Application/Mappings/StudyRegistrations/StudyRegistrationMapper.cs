using Application.Studies.V1.Commands.StudyRegistrations;
using Dte.Study.Management.Api.Client.Request.StudyRegistrations;

namespace Application.Mappings.StudyRegistrations
{
    public static class StudyRegistrationMapper
    {
        public static ApproveStudyRegistrationRequest MapTo(ApproveStudyRegistrationCommand source)
        {
            return new ApproveStudyRegistrationRequest
            {
                Title = source.Title,
                CpmsId = source.CpmsId,
                IsrctnId = source.IsrctnId,
                UserId = source.UserId
            };
        }
    }
}