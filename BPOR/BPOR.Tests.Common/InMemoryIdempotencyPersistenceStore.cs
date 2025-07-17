using System.Collections.Concurrent;
using AWS.Lambda.Powertools.Idempotency.Exceptions;
using AWS.Lambda.Powertools.Idempotency.Persistence;

namespace BPOR.Tests.Common
{
    internal class InMemoryIdempotencyPersistenceStore : BasePersistenceStore
    {
        private readonly Dictionary<string, DataRecord> _data = new();
        public override async Task DeleteRecord(string idempotencyKey)
        {
            lock (_data)
            {
                _data.Remove(idempotencyKey);
            }
        }

        public override async Task<DataRecord> GetRecord(string idempotencyKey)
        {
            lock (_data)
            {
                return _data.TryGetValue(idempotencyKey, out var record) ? record : null;
            }
        }

        public override async Task PutRecord(DataRecord record, DateTimeOffset now)
        {
            lock (_data)
            {
                if (_data.TryGetValue(record.IdempotencyKey, out var existingRecord))
                {
                    if (existingRecord.InProgressExpiryTimestamp < now.ToUnixTimeMilliseconds())
                    {
                        _data[record.IdempotencyKey] = record;
                    }
                    else
                    {
                        throw new IdempotencyItemAlreadyExistsException();
                    }
                }
                else
                {
                    _data.Add(record.IdempotencyKey, record);
                }
            }
        }

        public override async Task UpdateRecord(DataRecord record)
        {
            lock(_data)
            {
                _data[record.IdempotencyKey] = record;
            }
        }
    }
}