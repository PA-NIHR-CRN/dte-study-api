using Bogus;
using BPOR.Domain.Entities;

namespace BPOR.Tests.Common.Fakers;

public class ParticipantFaker : Faker<Participant>
{
    public ParticipantFaker() : base("en_GB")
    {
        RuleFor(i => i.FirstName, f => f.Name.FirstName());
        RuleFor(i => i.LastName, f => f.Name.LastName());
        RuleFor(i => i.Email, (f, i) => f.Internet.Email(i.FirstName, i.LastName));
        RuleFor(i => i.DateOfBirth, f => f.Date.Between(new DateTime(1940, 1, 1), new DateTime(2006, 1, 1)));
        RuleFor(i => i.Address, f => new ParticipantAddress
        {
            AddressLine1 = f.Address.StreetAddress(),
            Town = f.Address.City(),
            Postcode = f.Address.ZipCode()
        });
        RuleFor(i => i.Stage2CompleteUtc, (f, i) => f.Random.Bool(0.8f) ?
            f.Date.Between(new DateTime(2022, 1, 1), new DateTime(2025, 1, 1)) : null);
    }
}