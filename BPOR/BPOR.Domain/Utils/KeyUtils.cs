namespace BPOR.Domain.Utils;

public static class KeyUtils
{
    public static string DeletedKey(Guid primaryKey) => $"DELETED#{primaryKey}";
    public static string DeletedKey() => "DELETED#";
    public static string StripPrimaryKey(string pk) => pk.Replace("PARTICIPANT#", "");
    public static string ParticipantKey(string participantId) => $"PARTICIPANT#{participantId}";
    public static string ParticipantKey() => "PARTICIPANT#";
}
