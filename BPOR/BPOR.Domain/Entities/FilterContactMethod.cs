using BPOR.Domain.Entities.RefData;
using NIHR.NotificationService.Enums;

namespace BPOR.Domain.Entities
{
    public class FilterContactMethod
    {
        public int Id { get; set; }
        public int FilterCriteriaId { get; set; }
        public int ContactMethodId { get; set; }
        public GovUkNotifyContactMethod ContactMethod { get; set; }
    }
}
