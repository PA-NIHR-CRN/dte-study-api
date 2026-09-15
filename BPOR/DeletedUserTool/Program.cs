using System.Text;
using System.Text.Json;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using DeletedUserTool;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.user.json");

builder.Services.AddOptions<MySqlSettings>().BindConfiguration("RmsDb");
builder.Services.AddOptions<CognitoSettings>().BindConfiguration("BporCognito");
builder.Services.AddOptions<DynamoDbSettings>().BindConfiguration("DynamoDb");

builder.Services.AddSingleton<BporCognito>();
builder.Services.AddSingleton<RmsDatabase>();
builder.Services.AddSingleton<BporDynamoDb>();

var host = builder.Build();

var rmsDatabase = host.Services.GetRequiredService<RmsDatabase>();
var deletedParticipants = rmsDatabase.GetDeletedParticipants().Take(1);

var bporCognito = host.Services.GetRequiredService<BporCognito>();
var bporDynamoDb = host.Services.GetRequiredService<BporDynamoDb>();

List<DynamoParticipant> dynamoParticipantsByEmail = await bporDynamoDb.GetParticipantsByEmail(
    deletedParticipants.Select(i => i.Key.Email).Distinct());

var cognitoSettings = host.Services.GetRequiredService<IOptions<CognitoSettings>>();
var dynamoDbSettings = host.Services.GetRequiredService<IOptions<DynamoDbSettings>>();

string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"output-{DateTime.UtcNow:yyyy-MM-ddTHH.mm.ss}");
string scriptsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");
Directory.CreateDirectory(outputFolder);

using var cognitoScript = File.CreateText(Path.Combine(outputFolder, "cognito-clean.ps1"));
using var dynamoDbScript = File.CreateText(Path.Combine(outputFolder, "dynamo-db-clean.ps1"));
using var rmsDbScript = File.CreateText(Path.Combine(outputFolder, "rms-db-clean.sql"));

List<EmailAddressAudit> audits = new();
HashSet<string> handledDynamoDbPks = new();

foreach (var deletedEmail in deletedParticipants.GroupBy(i => i.Key.Email))
{
    string emailAddress = deletedEmail.Key;
    cognitoScript.WriteLine($"######## EMAIL: {emailAddress}");
    dynamoDbScript.WriteLine($"######## EMAIL: {emailAddress}");
    
    Console.WriteLine($"Found deleted RMS participant {emailAddress}");
    EmailAddressAudit emailAddressAudit = new() { Email = emailAddress };
    audits.Add(emailAddressAudit);

    var cognitoUsersMatchingEmail = await bporCognito.GetUserByEmail(emailAddress).ToArrayAsync();
    emailAddressAudit.CognitoUserIdsMatchingEmail = cognitoUsersMatchingEmail.Select(i => i.Username).ToArray();
    foreach (var cognitoUser in cognitoUsersMatchingEmail)
    {
        WriteCongnitoDelete(cognitoUser.Username);
        Console.WriteLine($"    with Cognito User {cognitoUser.Username}");
    }

    var dynamoParticipantsMatchingEmail = dynamoParticipantsByEmail.Where(i => i.Email == emailAddress);
    emailAddressAudit.DynamoDbRecordsMatchingEmail.AddRange( dynamoParticipantsMatchingEmail.Select(i => new DynamoDbAudit(i)));
    foreach (var dynamoParticipant in dynamoParticipantsMatchingEmail)
    {
        if (dynamoParticipant.Pk.StartsWith("PARTICIPANT#"))
        {
            WriteDynamoDbDelete(dynamoParticipant);
        }
        else if (dynamoParticipant.Pk.StartsWith("DELETED#"))
        {
            WriteDynamoDbAnonymise(dynamoParticipant);
        }
        Console.WriteLine(
            $"  with DynamoDB record by email {FormatRecord(dynamoParticipant)}");
    }
    
    foreach (var deletedRmsParticipant in deletedEmail)
    {
        int rmsParticipantId = deletedRmsParticipant.Key.Id;
        var rmsParticipantAudit = new RmsParticipantAudit(){Id = rmsParticipantId};
        emailAddressAudit.RmsParticipants.Add(rmsParticipantAudit);
        
        Console.WriteLine($"  with RMS participant ID {rmsParticipantId}");
        
        foreach (var participantIdentifierValue in deletedRmsParticipant)
        {
            RmsParticipantIdentifierAudit rmsParticipantIdentifierAudit = new()
            {
                Id = participantIdentifierValue.IdentifierId,
                IdentifierTypeId = participantIdentifierValue.IdentifierTypeId, 
                Value = participantIdentifierValue.IdentifierValue
            };
            rmsParticipantAudit.ParticipantIdentifierAudits.Add(rmsParticipantIdentifierAudit);
            string pkPrefix;
            switch (participantIdentifierValue.IdentifierTypeId)
            {
                case IdentifierTypes.Deleted:
                    pkPrefix = "DELETED";
                    break;
                case IdentifierTypes.ParticipantId:
                    pkPrefix = "PARTICIPANT";
                    break;
                case IdentifierTypes.NhsId:
                    pkPrefix = "PARTICIPANT";
                    break;
                default:
                    continue;
            }

            Console.WriteLine(
                $"    with Participant Identifier {participantIdentifierValue.IdentifierTypeId} {participantIdentifierValue.IdentifierValue}");

            var pk = $"{pkPrefix}#{participantIdentifierValue.IdentifierValue}";
            var sk = $"{pkPrefix}#";
            DynamoParticipant? dynamoRecord = await bporDynamoDb.GetParticipantByKey(pk, sk);
            if (dynamoRecord != null)
            {
                switch (participantIdentifierValue.IdentifierTypeId)
                {
                    case IdentifierTypes.ParticipantId:
                    case IdentifierTypes.NhsId:
                        WriteDynamoDbDelete(dynamoRecord);
                        break;
                    case IdentifierTypes.Deleted:
                        WriteDynamoDbAnonymise(dynamoRecord);
                        break;
                }
            }

            rmsParticipantIdentifierAudit.DynamoDbRecords.Add(new(dynamoRecord));
            Console.WriteLine(
                $"      with DynamoDB record by PK {FormatRecord(dynamoRecord)}");
        }
    }
}

static string FormatRecord(DynamoParticipant dynamoParticipant) =>
    $"PK:{dynamoParticipant.Pk} SK:{dynamoParticipant.Sk} Email:{dynamoParticipant.Email} NhsNo:{dynamoParticipant.NhsNumber} NhsId:{dynamoParticipant.NhsId}";

File.WriteAllText(Path.Combine(outputFolder, "audit.json"), JsonSerializer.Serialize(audits));

string[] scriptsToCopy = ["expression-attribute-names.json", "expression-attribute-values.json"];
foreach (var scriptToCopy in scriptsToCopy)
{
    File.Copy(Path.Combine(scriptsFolder, scriptToCopy), Path.Combine(outputFolder, scriptToCopy));
}

void WriteCongnitoDelete(string cognitoUserNameToRemove)
{
    cognitoScript.WriteLine($"aws cognito-idp admin-delete-user --profile {cognitoSettings.Value.Profile} --user-pool-id {cognitoSettings.Value.UserPoolId} --username {cognitoUserNameToRemove}");
}

void WriteDynamoDbAnonymise(DynamoParticipant dynamoParticipant)
{
    if (handledDynamoDbPks.Add(dynamoParticipant.Pk))
    {
        dynamoDbScript.WriteLine($"aws dynamodb update-item --profile {dynamoDbSettings.Value.Profile} --region {dynamoDbSettings.Value.RegionEndpoint} --table-name {dynamoDbSettings.Value.TableName} --key '{{\"PK\":{{\"S\":\"{dynamoParticipant.Pk}\"}}, \"SK\":{{\"S\": \"{dynamoParticipant.Sk}\"}}}}'}}' --expression-attribute-names file://expression-attribute-names.json --update-expression \"DELETE #NI, #NN, #E, #FN, #LN, #HCI, #MN, #LLN\"");
        using (var valueStream = File.Create(Path.Combine(outputFolder, $"{dynamoParticipant.Pk}.values.json")))
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

        dynamoDbScript.WriteLine($"aws dynamodb update-item --profile {dynamoDbSettings.Value.Profile} --region {dynamoDbSettings.Value.RegionEndpoint} --table-name {dynamoDbSettings.Value.TableName} --key '{{\"PK\":{{\"S\":\"{dynamoParticipant.Pk}\"}}, \"SK\":{{\"S\": \"{dynamoParticipant.Sk}\"}}}}'}}' --expression-attribute-names file://expression-attribute-names.json --expression-attribute-values file://{dynamoParticipant.Pk}.values.json --update-expression \"UPDATE #A = :a\"");
        dynamoDbScript.WriteLine();
    }
}

void WriteDynamoDbDelete(DynamoParticipant dynamoParticipant)
{
    if (handledDynamoDbPks.Add(dynamoParticipant.Pk))
    {
        dynamoDbScript.WriteLine(
            $"aws dynamodb delete-item --profile {dynamoDbSettings.Value.Profile} --region {dynamoDbSettings.Value.RegionEndpoint} --table-name {dynamoDbSettings.Value.TableName} --key '{{\"PK\":{{\"S\":\"{dynamoParticipant.Pk}\"}}, \"SK\":{{\"S\": \"{dynamoParticipant.Sk}\"}}}}'}}'");
    }
}