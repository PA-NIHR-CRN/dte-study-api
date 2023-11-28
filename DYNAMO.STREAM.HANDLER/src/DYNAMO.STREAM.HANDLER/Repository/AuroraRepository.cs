using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using Microsoft.EntityFrameworkCore;

namespace DYNAMO.STREAM.HANDLER.Repository;

public class AuroraRepository : IAuroraRepository
{
    private readonly ParticipantDbContext _dbContext;
    private readonly IRefDataService _refDataService;

    public AuroraRepository(ParticipantDbContext dbContext, IRefDataService refDataService)
    {
        _dbContext = dbContext;
        _refDataService = refDataService;
    }

    public async Task<Participant?> GetParticipantAsync(Participant participant, CancellationToken cancellationToken)
    {
        var identifierValues = participant.ParticipantIdentifiers.Select(y => y.Value).ToList();

        return await _dbContext
            .ParticipantIdentifiers
            .Where(x => identifierValues.Contains(x.Value))
            .Select(x => x.Participant)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Participant?> GetParticipantByPkAsync(string pk, CancellationToken cancellationToken)
    {
        return await _dbContext.Participants
            .Where(x => x.ParticipantIdentifiers.Any(y => y.Value == pk))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public bool IsLinkedAccount(Participant participant)
    {
        var linkedIdentifierTypes = new[]
        {
            _refDataService.GetIdentifierTypeId("ParticipantId"),
            _refDataService.GetIdentifierTypeId("NhsId")
        };

        return Array.TrueForAll(linkedIdentifierTypes, x => participant.ParticipantIdentifiers.Any(y => y.Id == x));
    }
}
