using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NIHR.Infrastructure.Paging
{
    public class Page<T> : Page, IEnumerable<T>
    {
        private ICollection<T> _items;

        public Page(ICollection<T> source, int pageSize, int currentPage, long totalCount) : base(source, pageSize, currentPage, totalCount)   
        {
            _items = source;
        }

        public ICollection<T> Items { get => _items; }

        public new IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    }

    public abstract class Page : IPage
    {
        private long _totalCount;
        private IEnumerable _items;
        private int _pageSize;
        private int _currentPage;

        public Page(IEnumerable source, int pageSize, int currentPage, long totalCount)
        {
            _totalCount = totalCount;
            _items = source;
            _pageSize = pageSize;
            _currentPage = currentPage;
        }

        public int CurrentPage { get => _currentPage; }
        public int PageSize { get => _pageSize; }
        public long TotalCount { get => _totalCount; }

        public long TotalPages => (long)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public int CurrentCount() => _items.Cast<object>().Count();

        public IEnumerator GetEnumerator() => _items.GetEnumerator();
    }

    public interface IPage : IEnumerable
    {
        public int CurrentPage { get; }
        public int CurrentCount();
        public int PageSize { get; }
        public long TotalCount { get; }
        public long TotalPages { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
    }
}