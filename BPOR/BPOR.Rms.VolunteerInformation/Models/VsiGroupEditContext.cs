namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiGroupEditContext : VsiEditContext
{
    public int GroupId { get; set; }

    public override Dictionary<string, string> ToRouteData()
    {
        var result = base.ToRouteData();
        result.Add(nameof(GroupId), GroupId.ToString());
        return result;
    }
}