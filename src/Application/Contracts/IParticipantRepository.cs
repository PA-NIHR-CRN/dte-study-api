using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Participants;

namespace Application.Contracts
{
    public interface IParticipantRepository : IAsyncEnumerable<Participant>
    {
        Task<ParticipantDetails> GetParticipantDetailsAsync(string participantId,
            CancellationToken cancellationToken = default);

        Task<ParticipantDemographics> GetParticipantDemographicsAsync(string participantId,
            CancellationToken cancellationToken = default);

        Task CreateParticipantDetailsAsync(ParticipantDetails entity, CancellationToken cancellationToken = default);

        Task UpdateParticipantDetailsAsync(ParticipantDetails entity, CancellationToken cancellationToken = default);

        Task CreateParticipantDemographicsAsync(ParticipantDemographics entity,
            CancellationToken cancellationToken = default);

        Task AddDemographicsToNhsUserAsync(ParticipantDemographics entity, string nhsId,
            CancellationToken cancellationToken = default);

        Task UpdateParticipantDemographicsAsync(ParticipantDemographics entity,
            CancellationToken cancellationToken = default);

        Task DeleteParticipantDetailsAsync(ParticipantDetails entity, CancellationToken cancellationToken = default);

        Task CreateAnonymisedDemographicParticipantDataAsync(ParticipantDetails entity,
            CancellationToken cancellationToken = default);

        Task<ParticipantDetails> QueryIndexForParticipantDetailsAsync(string query, string colName,
            CancellationToken cancellationToken = default);

        Task CreateParticipantAsync(Participant entity, CancellationToken cancellationToken = default);
        Task<Participant> GetParticipantAsync(string pk, CancellationToken cancellationToken = default);
        Task DeleteParticipantAsync(Participant entity, CancellationToken cancellationToken = default);
    }
}