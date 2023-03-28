using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IPrivateKeyProvider
    {
        Task<RSA> GetPrivateKeyAsync(CancellationToken cancellationToken);
    }
}
