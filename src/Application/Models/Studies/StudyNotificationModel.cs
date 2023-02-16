using System;
using System.Collections.Generic;

namespace Application.Models.Studies
{
    public class StudyNotificationModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        private List<InstructionModel> Instructions { get; set; } = new List<InstructionModel>();
        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime? ExpiryDateTimeUtc { get; set; }
    }
}