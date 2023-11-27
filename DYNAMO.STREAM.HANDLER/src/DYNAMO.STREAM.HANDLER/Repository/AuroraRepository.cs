using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using Microsoft.EntityFrameworkCore;

namespace DYNAMO.STREAM.HANDLER.Repository;

public class AuroraRepository : IAuroraRepository
{
    private readonly ParticipantDbContext _dbContext;

    public AuroraRepository(ParticipantDbContext dbContext)
    {
        _dbContext = dbContext;
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

    public async Task<Participant?> GetParticipantByIdAsync(string pk, CancellationToken cancellationToken)
    {
        return await _dbContext.Participants
            .Where(x => x.ParticipantIdentifiers.Any(y => y.Value == pk))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsLinkedAccountAsync(Participant participant, CancellationToken cancellationToken)
    {
        if (participant.ParticipantIdentifiers.Count == 2)
        {
            var participantFromDb = await GetParticipantAsync(participant, cancellationToken);
            return participantFromDb != null;
        }

        return false;
    }
}
