using System;

namespace Application.Models.Studies
{
    public class InstructionModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public bool Optional { get; set; }
        public string MoreInformation { get; set; }
        public Uri ExternalLink { get; set; }
    }
}