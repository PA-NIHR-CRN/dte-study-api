using System.Text;
using System.Text.Json;
using BPOR.Domain.Entities;
using Microsoft.Extensions.Options;

namespace DeletedUserTool.Writers;

public class DynamoDbScriptWriter : PowershellScriptWriter
{
    private readonly IOptions<DynamoDbSettings> _dynamoDbSettings;
    private readonly string _outputFolderPath;
    private readonly HashSet<string> _handledDynamoDbPks = new();

    public DynamoDbScriptWriter(IOptions<DynamoDbSettings> dynamoDbSettings, string outputFolderPath)
     : base(File.CreateText(Path.Combine(outputFolderPath, "dynamo-db-clean.ps1")))
    {
        _dynamoDbSettings = dynamoDbSettings;
        _outputFolderPath = outputFolderPath;
    }

    public void WriteDynamoDbAnonymise(DynamoParticipant dynamoParticipant)
    {
        if (_handledDynamoDbPks.Add(dynamoParticipant.Pk))
        {
            TextWriter.WriteLine(
                $"aws dynamodb update-item --profile {_dynamoDbSettings.Value.Profile} --region {_dynamoDbSettings.Value.RegionEndpoint} --table-name {_dynamoDbSettings.Value.TableName} --key '{GetKeyJson(dynamoParticipant)}' --expression-attribute-names file://expression-attribute-names.json --update-expression \"DELETE #NI, #NN, #E, #FN, #LN, #HCI, #MN, #LLN, #EB, #SRB\"");
            using (var valueStream =
                   File.Create(Path.Combine(_outputFolderPath, $"{dynamoParticipant.Pk}.values.json")))
            {
                using StreamWriter writer = new(valueStream, Encoding.ASCII);
                writer.Write(JsonSerializer.Serialize(new
                {
                    AddressPlaceHolder = new
                    {
                        Postcode = dynamoParticipant.Address.Postcode.Split(' ')[0],
                        Town = dynamoParticipant.Address.Town
                    }
                }).Replace("AddressPlaceHolder", ":a"));
            }

            TextWriter.WriteLine(
                $"aws dynamodb update-item --profile {_dynamoDbSettings.Value.Profile} --region {_dynamoDbSettings.Value.RegionEndpoint} --table-name {_dynamoDbSettings.Value.TableName} --key '{GetKeyJson(dynamoParticipant)}' --expression-attribute-names file://expression-attribute-names.json --expression-attribute-values file://{dynamoParticipant.Pk}.values.json --update-expression \"UPDATE #A = :a\"");
            TextWriter.WriteLine();
        }
    }

    public void WriteDynamoDbDelete(DynamoParticipant dynamoParticipant)
    {
        if (_handledDynamoDbPks.Add(dynamoParticipant.Pk))
        {
            TextWriter.WriteLine(
                $"aws dynamodb delete-item --profile {_dynamoDbSettings.Value.Profile} --region {_dynamoDbSettings.Value.RegionEndpoint} --table-name {_dynamoDbSettings.Value.TableName} --key '{GetKeyJson(dynamoParticipant)}'");
        }
    }

    private string GetKeyJson(DynamoParticipant dynamoParticipant) => JsonSerializer.Serialize(new
    {
        PK = new
        {
            S = dynamoParticipant.Pk
        },
        SK = new
        {
            S = dynamoParticipant.Sk
        }
    });
}