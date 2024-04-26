using BPOR.Domain.Entities.RefData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
