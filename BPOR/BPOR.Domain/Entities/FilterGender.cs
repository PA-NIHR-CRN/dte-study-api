using BPOR.Domain.Entities.RefData;

namespace BPOR.Domain.Entities
{
    public class FilterGender
    {
        public int Id { get; set; }
        public int FilterCriteriaId { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
    }
}
