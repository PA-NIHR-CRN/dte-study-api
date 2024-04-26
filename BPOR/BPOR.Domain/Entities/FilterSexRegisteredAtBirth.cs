using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPOR.Domain.Entities
{
    public class FilterSexRegisteredAtBirth
    {
        public int Id { get; set; }
        public int FilterCriteriaId { get; set; }
        public int YesNoPreferNotToSay { get; set; }
    }
}
