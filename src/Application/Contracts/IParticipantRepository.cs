using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Domain.Entities.Participants;

namespace Application.Contracts
{
    public interface IParticipantRepository
    {
        Task<ParticipantDetails> GetParticipantDetailsAsync(string participantId);
        Task<ParticipantDemographics> GetParticipantDemographicsAsync(string participantId);
        Task CreateParticipantDetailsAsync(ParticipantDetails entity);
        
        Task UpdateParticipantDetailsAsync(ParticipantDetails entity);
        Task CreateParticipantDemographicsAsync(ParticipantDemographics entity);
        Task AddDemographicsToNhsUserAsync(ParticipantDemographics entity, string nhsId);
        Task UpdateParticipantDemographicsAsync(ParticipantDemographics entity);
        Task DeleteParticipantDetailsAsync(ParticipantDetails entity);
        
        Task CreateAnonymisedDemographicParticipantDataAsync(ParticipantDetails entity);
        Task<ParticipantDetails> QueryIndexForParticipantDetailsAsync(string query, string colName);
        Task CreateParticipantAsync(Participant entity);
        IAsyncEnumerable<Participant> GetAllAsync(
            [EnumeratorCancellation] CancellationToken cancellationToken = default);

        Task<Participant> GetParticipantAsync(string pk);
        Task DeleteParticipantAsync(Participant entity);
    }
}