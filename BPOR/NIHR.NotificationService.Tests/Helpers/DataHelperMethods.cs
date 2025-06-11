using System.Runtime.CompilerServices;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using NIHR.NotificationService.Context;

namespace NIHR.NotificationService.Tests.Helpers;

internal static class DataHelperMethods
{

    public static void SetContactMethod(this Campaign campaign, ContactMethodId newContactMethod)
    {
        campaign.TypeId = newContactMethod;
        foreach (var participant in campaign.Participant)
        {
            participant.CampaignTypeId = newContactMethod;
        }
    }

    public static ICollection<NotificationData> AddIfNotNull(this ICollection<NotificationData> data, string key, string? value)
    {
        if (value != null)
            data.Add(key, value);
        return data;
    }

    public static ICollection<NotificationData> Add(this ICollection<NotificationData> data, string key, string value)
    {
        data.Add(new NotificationData { Key = key, Value = value });
        return data;
    }

    public static void RemoveByKey(this ICollection<NotificationData> datas, string key)
    {
        var datasToRemove = datas.Where(datas => datas.Key == key).ToList();
        foreach (var item in datasToRemove)  
            datas.Remove(item);
    }


    public static bool ValueIs<TValue>(this IDictionary<string, dynamic> data, string key, TValue expectedValue)
        => data.ValueIs<TValue>(key, typedValue => Equals(typedValue, expectedValue));

    public static bool ValueIs<TValue>(this IDictionary<string, dynamic> data, string key, Predicate<TValue>? predicate) 
        => data.TryGetValue(key, out var dynamicValue) && dynamicValue is TValue typedValue && (predicate?.Invoke(typedValue) ?? true);

    public static void AddParticipants (this Campaign campaign, IEnumerable<Participant> participants)
    {
        foreach (var participant in participants)
        {
            campaign.Participant.Add(new CampaignParticipant { 
                Participant = participant,
                CampaignTypeId = campaign.TypeId });
        }
    }

    public static void SetData (this Notification notification, string key, string value)
    {
        var notificationData = notification.NotificationDatas.SingleOrDefault(i => i.Key == key);
        if (value == null)
        {
            if (notificationData != null)
                notification.NotificationDatas.Remove(notificationData);
        }
        else
        {
            if (notificationData == null)
            {
                notification.NotificationDatas.Add(key, value);
            }
            else
            {
                notificationData.Value = value;
            }
        }
    }
}