using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace NIHR.Infrastructure.DependencyInjection;

public static class KeyedServiceExtensions
{
    public static void AddKeyedServiceDictionary(
        this IServiceCollection sc)
    {
        sc.AddSingleton(typeof(KeyedServiceCache<,>));
        sc.AddSingleton(sc);
        sc.AddTransient(typeof(IReadOnlyDictionary<,>), typeof(KeyedServiceDictionary<,>));
    }

    private sealed class KeyedServiceDictionary<TKey, TService> : IReadOnlyDictionary<TKey, TService>
        where TKey : notnull
        where TService : notnull
    {
        private readonly TKey[] _keys;
        private readonly IServiceProvider _provider;

        public KeyedServiceDictionary(KeyedServiceCache<TKey, TService> keyedServiceCache, IServiceProvider provider)
        {
            _keys = keyedServiceCache.Keys;
            _provider = provider;
        }

        public IEnumerator<KeyValuePair<TKey, TService>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        private class Enumerator : IEnumerator<KeyValuePair<TKey, TService>>
        {
            private readonly KeyedServiceDictionary<TKey, TService> _parent;
            private readonly IEnumerator<TKey> _enumerator;

            public Enumerator(KeyedServiceDictionary<TKey, TService> parent)
            {
                _parent = parent;
                _enumerator = parent.Keys.GetEnumerator();
            }

            public bool MoveNext() => _enumerator.MoveNext();

            public void Reset() => _enumerator.Reset();

            public KeyValuePair<TKey, TService> Current => new KeyValuePair<TKey, TService>(
                _enumerator.Current, _parent._provider.GetRequiredKeyedService<TService>(_enumerator.Current));

            object? IEnumerator.Current => Current;

            public void Dispose() => _enumerator.Dispose();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _keys.Length;

        public bool ContainsKey(TKey key) => _keys.Contains(key);

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TService value)
        {
            if (!ContainsKey(key))
            {
                value = default;
                return false;
            }

            value = _provider.GetRequiredKeyedService<TService>(key);
            return true;
        }

        public TService this[TKey key] => _provider.GetRequiredKeyedService<TService>(key);

        public IEnumerable<TKey> Keys => _keys.AsEnumerable();

        public IEnumerable<TService> Values
        {
            get
            {
                foreach (var key in Keys)
                {
                    yield return _provider.GetRequiredKeyedService<TService>(key);
                }
            }
        }
    }

    private sealed class KeyedServiceCache<TKey, TService>(IServiceCollection serviceCollection)
        where TKey : notnull
        where TService : notnull
    {
        public TKey[] Keys { get; } = 
            serviceCollection.Where(serviceDescriptor => 
                    serviceDescriptor.ServiceKey is TKey &&
                    serviceDescriptor.ServiceType == typeof(TService))
                .Select(serviceDescriptor => (TKey)serviceDescriptor.ServiceKey!)
                .ToArray();
    }
}