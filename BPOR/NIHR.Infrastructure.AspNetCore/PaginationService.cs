
using Microsoft.AspNetCore.Http;

namespace BPOR.Rms.Startup
{
    public class PaginationService : IPaginationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const int _defaultPageSize = 10;
        private const int _defaultPage = 1;

        public PaginationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Page => int.TryParse(_httpContextAccessor?.HttpContext?.Request.Query["page"], out var page) ? page : _defaultPage;


        public int PageSize => int.TryParse(_httpContextAccessor?.HttpContext?.Request.Query["pageSize"], out var pageSize) ? pageSize : _defaultPageSize;

    }
}
