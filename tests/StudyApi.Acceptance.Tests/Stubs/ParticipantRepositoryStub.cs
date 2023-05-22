using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain.Entities.Participants;

namespace ParticipantApi.Acceptance.Tests.Stubs;

public class ParticipantRepositoryStub : IParticipantRepository
{
    private ConcurrentBag<ParticipantDemographics> _participantDemographics;
    private ConcurrentBag<ParticipantDetails> _participantDetails;

    public ParticipantRepositoryStub()
    {
        _participantDetails = new ConcurrentBag<ParticipantDetails>();
        _participantDemographics = new ConcurrentBag<ParticipantDemographics>();
    }

    public async Task<ParticipantDetails> GetParticipantDetailsAsync(string participantId)
    {
        var detail = _participantDetails.FirstOrDefault(x => x.ParticipantId == participantId);

        return await Task.FromResult(detail);
    }

    public async Task<ParticipantDemographics> GetParticipantDemographicsAsync(string participantId)
    {
        var demographic = _participantDemographics.FirstOrDefault(x => x.ParticipantId == participantId);

        return await Task.FromResult(demographic);
    }

    public async Task CreateParticipantDetailsAsync(ParticipantDetails entity)
    {
        _participantDetails.Add(entity);

        await Task.CompletedTask;
    }

    public async Task UpdateParticipantDetailsAsync(ParticipantDetails entity)
    {
        var item = _participantDetails.FirstOrDefault(x => x.ParticipantId == entity.ParticipantId);

        if (item == null)
            throw new Exception(
                $"{nameof(ParticipantRepositoryStub)} can not find Participant details for id: {entity.ParticipantId}");

        var myBag = new ConcurrentBag<ParticipantDetails>(_participantDetails.Except(new[] { item }));

        _participantDetails = myBag; // TODO - check if this works

        await Task.CompletedTask;
    }

    public async Task CreateParticipantDemographicsAsync(ParticipantDemographics entity)
    {
        _participantDemographics.Add(entity);

        await Task.CompletedTask;
    }

    public Task AddDemographicsToNhsUserAsync(ParticipantDemographics entity, string nhsId)
    {
        _participantDemographics.Add(entity);

        return Task.CompletedTask;
    }

    public async Task UpdateParticipantDemographicsAsync(ParticipantDemographics entity)
    {
        var item = _participantDemographics.FirstOrDefault(x => x.ParticipantId == entity.ParticipantId);

        if (item == null)
            throw new Exception(
                $"{nameof(ParticipantRepositoryStub)} can not find Participant demographics for id: {entity.ParticipantId}");

        var myBag = new ConcurrentBag<ParticipantDemographics>(_participantDemographics.Except(new[] { item }));

        _participantDemographics = myBag; // TODO - check if this works

        await Task.CompletedTask;
    }

    public Task DeleteParticipantDetailsAsync(ParticipantDetails entity)
    {
        var list = _participantDetails.ToList();
        list.Remove(entity);

        _participantDetails = new ConcurrentBag<ParticipantDetails>(list);

        return Task.CompletedTask;
    }

    public Task CreateAnonymisedDemographicParticipantDataAsync(ParticipantDetails entity)
    {
        var list = _participantDetails.ToList();
        list.Remove(entity);

        _participantDetails = new ConcurrentBag<ParticipantDetails>(list);

        return Task.CompletedTask;
    }

    public Task<ParticipantDetails> QueryIndexForParticipantDetailsAsync(string query, string indexName)
    {
        throw new NotImplementedException();
    }

    public async Task<ParticipantDetails> GetParticipantDetailsByEmailAsync(string email)
    {
        var detail = _participantDetails.FirstOrDefault(x => x.Email == email);

        return await Task.FromResult(detail);
    }

    public async Task<ParticipantDetails> GetParticipantDetailsByNhsNumberAsync(string nhsNumber)
    {
        var detail = _participantDetails.FirstOrDefault(x => x.NhsNumber == nhsNumber);

        return await Task.FromResult(detail);
    }
}