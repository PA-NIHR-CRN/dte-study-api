using BPOR.Domain.Entities.RefData;

namespace BPOR.Domain.Entities
{
    public class FilterContactPreference
    {
        public int Id { get; set; }
        public int FilterCriteriaId { get; set; }
        public int ContactPreferenceId { get; set; }
        public ContactPreference ContactPreference { get; set; }

    }
}
