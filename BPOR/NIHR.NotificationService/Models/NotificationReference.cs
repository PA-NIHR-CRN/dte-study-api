using System.Diagnostics.CodeAnalysis;

namespace NIHR.NotificationService.Models;

public class NotificationReference
{
    public string? UpstreamReference { get; set; }
    public string UpstreamProviderKey { get; set; }

    public NotificationReference(string upstreamProviderKey, string? upstreamReference = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(upstreamProviderKey);

        UpstreamReference = upstreamReference;
        UpstreamProviderKey = upstreamProviderKey;
    }

    public static bool TryParse(string value, [MaybeNullWhen(false)] out NotificationReference result)
    {
        var parts = value.Split(':', 2);
        if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]))
        {
            result = null;
            return false;
        }
        
        result = new NotificationReference (parts[0], parts[1]);
        return true;
    }

    public override string ToString()
    {
        return $"{UpstreamProviderKey}:{UpstreamReference}";
    }
}