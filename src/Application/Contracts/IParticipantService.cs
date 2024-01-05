using System;
using System.Threading.Tasks;
using Application.Models.MFA;
using Application.Responses.V1.Participants;
using Domain.Entities.Participants;
using Dte.Common.Responses;

namespace Application.Contracts;

public interface IParticipantService
{
    Task<ParticipantDetails> GetParticipantDetailsAsync(string participantId);
    Task<ParticipantDetails> GetParticipantDetailsByEmailAsync(string email);
    Task<ParticipantDetails> GetParticipantDetailsByNhsNumberAsync(string nhsNumber);
    Task<ParticipantDemographicsResponse> GetParticipantDemographicsAsync(string participantId);
    Task CreateParticipantDetailsAsync(ParticipantDetails entity);
    Task UpdateParticipantDetailsAsync(ParticipantDetails entity);
    Task CreateParticipantDemographicsAsync(ParticipantDemographics entity);
    Task UpdateParticipantDemographicsAsync(ParticipantDemographics entity);
    Task NhsLoginAsync(ParticipantDetails entity);
    Task UpdateParticipantEmailAsync(string requestParticipantId, string requestNewEmail);
    Task<Response<object>> DeleteUserAsync(string requestParticipantId);
    Task StoreMfaCodeAsync(string username, string code);
    Task<MfaValidationResult> ValidateMfaCodeAsync(string username, string code);
}
