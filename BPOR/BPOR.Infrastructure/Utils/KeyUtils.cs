namespace BPOR.Infrastructure.Utils;

public static class KeyUtils
{
    public static string DeletedKey(Guid primaryKey) => $"DELETED#{primaryKey}";
    public static string DeletedKey() => "DELETED#";
    public static string StripPrimaryKey(string pk) => pk.Replace("PARTICIPANT#", "");
}
