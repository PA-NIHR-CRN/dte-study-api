using StudyApi.Requests.Studies;

namespace StudyApi.Requests.StudyRegistrations
{
    public class CreateStudyRegistrationRequest
    {
        public long StudyId { get; set; }
        public string Title { get; set; }
        public ResearcherRequest Researcher { get; set; }
    }
}