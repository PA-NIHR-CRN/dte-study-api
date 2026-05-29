namespace NIHR.Rts.Client;

public interface IRtsAddressSource
{
    Task<IEnumerable<RtsAddress>> SearchByPostcode(string postcode, CancellationToken cancellationToken);
    Task<RtsAddress?> GetById(int rtsAddressId, CancellationToken cancellationToken);
}