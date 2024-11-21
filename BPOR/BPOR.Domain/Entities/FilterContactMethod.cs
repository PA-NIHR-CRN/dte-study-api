using BPOR.Domain.Entities.RefData;

namespace BPOR.Domain.Entities
{
    public class FilterContactMethod
    {
        public int Id { get; set; }
        public int FilterCriteriaId { get; set; }
        public int ContactMethodId { get; set; }
        public ContactMethod ContactMethod { get; set; }
    }
}
