using BPOR.Domain.Entities;

public class DynamoDbAudit
{
    public DynamoDbAudit()
    {
    }

    public DynamoDbAudit(DynamoParticipant dynamoParticipant)
    {
        Email = dynamoParticipant.Email;
        NhsNumber =  dynamoParticipant.NhsNumber;
        NhsId = dynamoParticipant.NhsId;
        Pk = dynamoParticipant.Pk;
        Sk = dynamoParticipant.Sk;
    }

    public string Sk { get; set; }

    public string Pk { get; set; }

    public string NhsId { get; set; }

    public string NhsNumber { get; set; }

    public string Email { get; set; }
}