using BPOR.Domain;

namespace NIHR.Rts.Client;

public interface IRtsAddressSource
{
    Task<IEnumerable<RtsAddress>> SearchByPostcode(Postcode postcode, CancellationToken cancellationToken);
    Task<RtsAddress?> GetById(string rtsAddressId, CancellationToken cancellationToken);
}