using BPOR.Domain.Entities.RefData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
