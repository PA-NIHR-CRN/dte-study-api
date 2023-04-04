using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IClientAssertionJwtProvider
    {
        Task<string> CreateClientAssertionJwtAsync(CancellationToken cancellationToken);
        Task<string> CreateSSOJwtAsync(string jti, CancellationToken cancellationToken);
    }
}
