using BPOR.Domain.Entities.RefData;

namespace BPOR.Domain.Entities
{
    public class FilterAreaOfInterest
    {
        public int Id { get; set; }
        public int FilterCriteriaId { get; set; }
        public int HealthConditionId { get; set; }
        public HealthCondition HealthCondition { get; set; }
    }
}
