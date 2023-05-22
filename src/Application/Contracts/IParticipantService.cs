using System.Threading.Tasks;
using Application.Responses.V1.Participants;
using Domain.Entities.Participants;

namespace Application.Contracts;

public interface IParticipantService
{
    Task<ParticipantDetails> GetParticipantDetails(string participantId);
    Task<ParticipantDetails> GetParticipantDetailsByEmail(string email);
    Task<ParticipantDetails> GetParticipantDetailsByNhsNumber(string nhsNumber);
    Task<ParticipantDemographicsResponse> GetParticipantDemographics(string participantId);
    Task CreateParticipantDetails(ParticipantDetails entity);
    Task UpdateParticipantDetails(ParticipantDetails entity);
    Task CreateParticipantDemographics(ParticipantDemographics entity);
    Task UpdateParticipantDemographics(ParticipantDemographics entity);
    Task NhsLoginAsync(ParticipantDetails entity);
    Task UpdateParticipantEmail(string requestParticipantId, string requestNewEmail);
    Task DeleteUserAsync(string requestParticipantId);
}