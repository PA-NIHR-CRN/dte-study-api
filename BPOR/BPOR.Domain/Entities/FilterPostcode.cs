namespace BPOR.Domain.Entities
{
    public class FilterPostcode
    {
        public int Id { get; set; }
        public int FilterCriteriaId { get; set; }
        public string PostcodeFragment { get; set; }
    }
}
