using Application.Models.Studies;

namespace Application.Responses.V1.Studies
{
    public class StudyRoleResponse
    {
        public string Role { get; set; }
        public StudyResponse Item { get; set; }
    }
}