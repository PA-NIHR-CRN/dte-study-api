using StudyApi.Requests.Studies;

namespace StudyApi.Requests.StudyRegistrations
{
    public class ApproveStudyRegistrationRequest
    {
        public string Title { get; set; }
        public long CpmsId { get; set; }
        public string IsrctnId { get; set; }
    }
}