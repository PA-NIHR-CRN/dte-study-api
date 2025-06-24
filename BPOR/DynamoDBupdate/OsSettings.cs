
public class OsSettings
{
    public string Key { get; set; }
    public string CachePath { get; set; }
    public bool RunCanonicalTownBackfill { get; set; } = false;
    public bool RunStage2CompleteBackfill { get;  set; } = false;
    public bool RunStage2BackfillUpdate { get; set; } = false;

    public bool RunOsgbGridRefBackfill { get; set; } = false;
}