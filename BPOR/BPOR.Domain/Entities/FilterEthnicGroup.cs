using BPOR.Domain.Entities.RefData;

namespace BPOR.Domain.Entities
{
    public class FilterEthnicGroup
    {
        public int Id { get; set; }
        public int FilterCriteriaId { get; set; }
        public int EthnicGroupId { get; set; }
        public EthnicGroup EthnicGroup { get; set; }
    }
}
