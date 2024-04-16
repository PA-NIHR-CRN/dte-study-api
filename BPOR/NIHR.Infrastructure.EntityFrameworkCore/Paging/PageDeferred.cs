using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace NIHR.Infrastructure.EntityFrameworkCore.Paging
{
    public class PageDeferred<T>
    {
        private QueryFutureEnumerable<T> _items;
        private QueryFutureValue<long> _totalCount;
        private int _pageSize;
        private int _currentPage;

        public PageDeferred(QueryFutureEnumerable<T> source, int pageSize, int currentPage, QueryFutureValue<long> totalCount)
        {

            _items = source;
            _totalCount = totalCount;
            _pageSize = pageSize;
            _currentPage = currentPage;
        }

        public Page<T> Value => new Page<T>(_items.ToList(), _pageSize, _currentPage, _totalCount.Value);

        public async Task<Page<T>> ValueAsync(CancellationToken token = default) => new Page<T>(await _items.ToListAsync(token), _pageSize, _currentPage, await _totalCount.ValueAsync(token));

        public static implicit operator Page<T>(PageDeferred<T> source) => source.Value;

    }
}
