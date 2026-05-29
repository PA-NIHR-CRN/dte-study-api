namespace NIHR.Rts.Client;

public class TestRtsAddressSource : IRtsAddressSource
{
    private RtsAddress[] _sampleData =
    [
        new(1, "33 Church Lane", "Crossgar", "", "", "", "BT30 8TR"),
        new(2, "28 Church Lane", "Crossgar", "", "", "", "BT30 8TR"),
        new(3, "26a Church Lane", "Crossgar", "", "", "", "BT30 8TR"),
        new(4, "2 Donegall St", "Antrim", "", "", "", "BT45 1YU"),
        new(5, "4 Donegall St", "Antrim", "", "", "", "BT45 1YU"),
        new(6, "12 St. Joseph's Close", "Belfast", "", "", "", "BT1 3ST")
    ];


    public async Task<IEnumerable<RtsAddress>> SearchByPostcode(string postcode, CancellationToken cancellationToken)
    {
        return _sampleData.Where(i => i.Postcode.Replace(" ", "").Equals(postcode.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)).ToArray();
    }

    public Task<RtsAddress?> GetById(int rtsAddressId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_sampleData.SingleOrDefault(i => i.Id == rtsAddressId));
    }
}