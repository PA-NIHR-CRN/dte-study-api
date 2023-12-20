using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dynamo.Stream.Ingestor
{
    [JsonSerializable(typeof(ItemResponse))]
    public partial class ItemResponseSerializerContext : JsonSerializerContext
    {
    }
}
