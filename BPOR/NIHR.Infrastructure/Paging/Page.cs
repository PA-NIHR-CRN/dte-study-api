
using System;
using System.Collections;
using System.Collections.Generic;

namespace NIHR.Infrastructure.Paging
{
    public class Page<T> : Page, IEnumerable<T>, IEnumerable
    {
        private ICollection<T> _items;

        public Page(ICollection<T> source, int pageSize, int currentPage, long totalCount) : base(pageSize, currentPage, totalCount)   
        {
            _items = source;
        }

        public ICollection<T> Items { get => _items; }

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
    }

    public class Page
    {
        private long _totalCount;
        private int _pageSize;
        private int _currentPage;

        public Page(int pageSize, int currentPage, long totalCount)
        {
            _totalCount = totalCount;
            _pageSize = pageSize;
            _currentPage = currentPage;
        }

        public int CurrentPage { get => _currentPage; }
        public int PageSize { get => _pageSize; }
        public long TotalCount { get => _totalCount; }

        public long TotalPages => (long)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}