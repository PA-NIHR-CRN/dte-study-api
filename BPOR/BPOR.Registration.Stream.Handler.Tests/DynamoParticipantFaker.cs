
using Bogus;
using BPOR.Domain.Entities;

namespace BPOR.Registration.Stream.Handler.Tests;

public class DynamoParticipantFaker : Faker<DynamoParticipant>
{
    public DynamoParticipantFaker() : base("en_GB")
    {
        RuleFor(i => i.Pk, f => f.Random.Guid().ToString());
        RuleFor(i => i.Firstname, f => f.Name.FirstName());
        RuleFor(i => i.Lastname, f => f.Name.LastName());
        RuleFor(i => i.Email, (f, i) => f.Internet.Email(i.Firstname, i.Lastname));
        RuleFor(i => i.DateOfBirth, f => f.Date.Between(new DateTime(1940, 1, 1), new DateTime(2006, 1, 1)));
        RuleFor(i => i.NhsId, f => f.Random.Guid().ToString());
        RuleFor(i => i.ParticipantId, f => f.Random.Guid().ToString());
        RuleFor(i => i.Address, f => new DynamoParticipantAddress
        {
            AddressLine1 = f.Address.StreetAddress(),
            Town = f.Address.City(),
            Postcode = f.Address.ZipCode()
        });
    }
}