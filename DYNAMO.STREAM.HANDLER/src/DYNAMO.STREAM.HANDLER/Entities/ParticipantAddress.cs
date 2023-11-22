using System.ComponentModel.DataAnnotations;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantAddress
{
    [Key] 
    public int Id { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string AddressLine4 { get; set; }
    public string Town { get; set; }
    public string Postcode { get; set; }
    public int ParticipantId { get; set; }
    public Participant Participant { get; set; }
}
