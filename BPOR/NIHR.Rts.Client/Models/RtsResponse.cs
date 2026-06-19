namespace NIHR.Rts.Client;

public sealed class RtsResponse
{
    public RtsResult Result { get; init; } = null!;
}

public sealed class RtsResult
{
    public IEnumerable<RtsAddress> RtsOrganisations { get; init; }
        = Enumerable.Empty<RtsAddress>();
}