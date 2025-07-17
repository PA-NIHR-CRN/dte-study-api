using System.Reflection;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;

namespace BPOR.Tests.Common
{
    public static class DynamoDbExtensions
    {
        public static DynamoDBEvent.DynamodbStreamRecord CreateInsertStreamRecord<T>(this DynamoDBContext dynamoDbContext, T entity, string eventId)
        {
            var document = dynamoDbContext.ToDocument(entity);
            var map = document.ToAttributeMap();

            return new DynamoDBEvent.DynamodbStreamRecord()
            {
                EventID = eventId,
                EventName = OperationType.INSERT,
                Dynamodb = new StreamRecord
                {
                    Keys = GetKeyAttributeValues<T>(map),
                    NewImage = map
                }
            };
        }

        public static Dictionary<string, AttributeValue> GetKeyAttributeValues<T>(Dictionary<string, AttributeValue> map)
        {
            var keyNames = new List<string>();
            keyNames.AddRange(typeof(T).GetProperties().Select(i => i.GetCustomAttribute<DynamoDBHashKeyAttribute>()).Where(attribute => attribute != null).Select(attribute => attribute.AttributeName));
            keyNames.AddRange(typeof(T).GetProperties().Select(i => i.GetCustomAttribute<DynamoDBRangeKeyAttribute>()).Where(attribute => attribute != null).Select(a => a.AttributeName));

            return map.Where(i => keyNames.Contains(i.Key)).ToDictionary(i => i.Key, i => i.Value);
        }
    }
}
