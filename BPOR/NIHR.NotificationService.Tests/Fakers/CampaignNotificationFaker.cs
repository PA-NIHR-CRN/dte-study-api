
using Bogus;
using BPOR.Domain.Entities;
using BPOR.Rms.Constants;
using NIHR.NotificationService.Context;
using NIHR.NotificationService.Tests.Helpers;

namespace NIHR.NotificationService.Tests.Fakers;

public class CampaignNotificationFaker : Faker<Notification>
{
    private readonly IEnumerator<CampaignParticipant> _participantEnumerator;

    public CampaignNotificationFaker(IEnumerable<CampaignParticipant> participants)
    {
        _participantEnumerator = participants.GetEnumerator();

        Rules((faker, notification) =>
        {

            if (!_participantEnumerator.MoveNext())
                throw new Exception("Participants exhausted");

            CampaignParticipant campaignParticipant = _participantEnumerator.Current;
            notification.PrimaryIdentifier = campaignParticipant.Participant.Email;

            notification.NotificationDatas.Add(PersonalisationKeys.CampaignParticipantId, campaignParticipant.Id.ToString());
            notification.NotificationDatas.Add(PersonalisationKeys.CampaignTypeId, ((int)campaignParticipant.CampaignTypeId).ToString());
            notification.NotificationDatas.AddIfNotNull(PersonalisationKeys.Email, campaignParticipant.Participant.Email);
            notification.NotificationDatas.AddIfNotNull(PersonalisationKeys.FirstName, campaignParticipant.Participant.FirstName);
            notification.NotificationDatas.AddIfNotNull(PersonalisationKeys.LastName, campaignParticipant.Participant.LastName);
            notification.NotificationDatas.Add(PersonalisationKeys.UniqueLink, $"https://engage.testdomain.com/?reference={faker.Random.AlphaNumeric(8)}");
            notification.NotificationDatas.Add(PersonalisationKeys.TemplateId, faker.Random.Guid().ToString());
            notification.NotificationDatas.Add(PersonalisationKeys.UniqueReference, faker.Random.Int(100_000, 999_999).ToString());
            notification.NotificationDatas.AddIfNotNull("address_line_1", campaignParticipant.Participant.Address?.AddressLine1);
            notification.NotificationDatas.AddIfNotNull("address_line_2", campaignParticipant.Participant.Address?.AddressLine2);
            notification.NotificationDatas.AddIfNotNull("address_line_3", campaignParticipant.Participant.Address?.AddressLine3);
            notification.NotificationDatas.AddIfNotNull("address_line_4", campaignParticipant.Participant.Address?.AddressLine4);
            notification.NotificationDatas.AddIfNotNull("address_line_5", campaignParticipant.Participant.Address?.Town);
            notification.NotificationDatas.AddIfNotNull("address_line_6", campaignParticipant.Participant.Address?.Postcode);
        });
    }
}