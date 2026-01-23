using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Microsoft.Extensions.Caching.Distributed;

public class NhsLoginStateStore : INhsLoginStateStore
{
    private readonly IDistributedCache _cache;

    public NhsLoginStateStore(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task StoreAsync(string state, string nonce, CancellationToken ct)
    {
        await _cache.SetStringAsync(
            $"nhslogin:{state}",
            nonce,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            },
            ct
        );
    }

    public async Task<string?> GetAndDeleteAsync(string state, CancellationToken ct)
    {
        var key = $"nhslogin:{state}";
        var nonce = await _cache.GetStringAsync(key, ct);

        if (nonce != null)
            await _cache.RemoveAsync(key, ct);

        return nonce;
    }
}
