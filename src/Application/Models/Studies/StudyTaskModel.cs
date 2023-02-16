using System;
using System.Collections.Generic;

namespace Application.Models.Studies
{
    public class StudyTaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        private List<InstructionModel> Instructions { get; set; } = new List<InstructionModel>();
        public DateTime ActivationDateTimeUtc { get; set; }
        public bool Enabled { get; set; } = true;
    }
}