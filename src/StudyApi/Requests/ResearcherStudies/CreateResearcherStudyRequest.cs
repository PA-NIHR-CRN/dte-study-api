using Domain.Entities.Studies;

namespace StudyApi.Requests.ResearcherStudies
{
    public class CreateResearcherStudyRequest
    {
        public string ResearcherId { get; set; }
        public string ResearcherFirstname { get; set; }
        public string ResearcherLastname { get; set; }
        public string ResearcherEmail { get; set; }
        public long StudyId { get; set; }
        public ResearcherStudyRole Role { get; set; }
    }
}