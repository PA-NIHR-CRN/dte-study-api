using BPOR.Domain.Entities;

namespace BPOR.Rms.Models.Volunteer;

public class CampaignParticipantDetails
{
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ParticipantAddress? Address { get; set; }
}
