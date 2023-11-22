using Amazon.Lambda.DynamoDBEvents;
using DYNAMO.STREAM.HANDLER.Entities;

namespace DYNAMO.STREAM.HANDLER.Mappers;

public static class ParticipantMapper
{
    public static Participant Map(DynamoDBEvent.DynamodbStreamRecord record)
    {
        var source = record.Dynamodb.NewImage;
        var participant = new Participant
        {
            ParticipantId = source["ParticipantId"].S,
            Email = source["Email"].S,
            FirstName = source["Firstname"].S,
            LastName = source["Lastname"].S,
            RegistrationConsent = source["ConsentRegistration"].BOOL,
            RegistrationConsentAtUtc = DateTime.Parse(source["RegistrationConsentAtUtc"].S),
            MobileNumber = source["MobileNumber"].S,
            LandlineNumber = source["LandlineNumber"].S,
            // Address = ParticipantAddressMapper.Map(source["Address"].S),
            DateOfBirth = DateTime.Parse(source["DateOfBirth"].S),
            // SexRegisteredAtBirthId = MapDynamoParticipantToSexRegisteredRefData(source["SexRegisteredAtBirth"].S),
            GenderIsSameAsSexRegisteredAtBirth = source["GenderIsSameAsSexRegisteredAtBirth"].BOOL,
            EthnicGroup = source["EthnicGroup"].S,
            EthnicBackground = source["EthnicBackground"].S,
            Disability = source["Disability"].BOOL,
            DisabilityDescription = source["LandlineNumber"].S,
            CreatedAt = DateTime.Parse(source["CreatedAtUtc"].S),
            UpdatedAt = DateTime.Parse(source["UpdatedAtUtc"].S),
        };
        return participant;
    }

    private static int MapDynamoParticipantToSexRegisteredRefData(string sex)
    {
        throw new NotImplementedException();
    }
}
