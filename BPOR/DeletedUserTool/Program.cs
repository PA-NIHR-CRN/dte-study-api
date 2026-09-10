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
var deletedParticipants = rmsDatabase.GetDeletedParticipants().Take(10);

var bporCognito = host.Services.GetRequiredService<BporCognito>();
var bporDynamoDb = host.Services.GetRequiredService<BporDynamoDb>();

List<DynamoParticipant> dynamoParticipantsByEmail = await bporDynamoDb.GetParticipantsByEmail(deletedParticipants.Select(i => i.Key.Email).Distinct());

HashSet<string> dynamoDbPksToDelete = new();
HashSet<string> dynamoDbPksToAnonymise = new();
HashSet<string> cognitoUsernamesToRemove = new();

List<EmailAddressAudit> audits = new();

foreach (var deletedEmail in deletedParticipants.GroupBy(i => i.Key.Email))
{
    string emailAddress = deletedEmail.Key;
    Console.WriteLine($"Found deleted RMS participant {emailAddress}");
    EmailAddressAudit emailAddressAudit = new() { Email = emailAddress };
    audits.Add(emailAddressAudit);

    var cognitoUsersMatchingEmail = await bporCognito.GetUserByEmail(emailAddress).ToArrayAsync();
    emailAddressAudit.CognitoUserIdsMatchingEmail = cognitoUsersMatchingEmail.Select(i => i.Username).ToArray();
    foreach (var cognitoUser in cognitoUsersMatchingEmail)
    {
        cognitoUsernamesToRemove.Add(cognitoUser.Username);
        Console.WriteLine(
            $"    with Cognito User {cognitoUser.Username}");
    }

    var dynamoParticipantsMatchingEmail = dynamoParticipantsByEmail.Where(i => i.Email == emailAddress);
    emailAddressAudit.DynamoDbRecordsMatchingEmail.AddRange( dynamoParticipantsMatchingEmail.Select(i => new DynamoDbAudit(i)));
    foreach (var dynamoParticipant in dynamoParticipantsMatchingEmail)
    {
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

            switch (participantIdentifierValue.IdentifierTypeId)
            {
                case IdentifierTypes.ParticipantId:
                case IdentifierTypes.NhsId:
                    dynamoDbPksToDelete.Add(pk);
                    break;
                case IdentifierTypes.Deleted:
                    dynamoDbPksToAnonymise.Add(pk);
                    break;
            }

            var dynamoParticipantsByPk = await bporDynamoDb.GetParticipantsByPk(pk);
            foreach (var dynamoParticipant in dynamoParticipantsByPk)
            {
                rmsParticipantIdentifierAudit.DynamoDbRecords.Add(new(dynamoParticipant));
                Console.WriteLine(
                    $"      with DynamoDB record by PK {FormatRecord(dynamoParticipant)}");
            }
        }

    }
}

static string FormatRecord(DynamoParticipant dynamoParticipant) =>
    $"PK:{dynamoParticipant.Pk} SK:{dynamoParticipant.Sk} Email:{dynamoParticipant.Email} NhsNo:{dynamoParticipant.NhsNumber} NhsId:{dynamoParticipant.NhsId}";

string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"output-{DateTime.UtcNow:yyyy-MM-ddTHH.mm.ss}");
string scriptsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");

Directory.CreateDirectory(outputFolder);
File.WriteAllText(Path.Combine(outputFolder, "audit.json"), JsonSerializer.Serialize(audits));

using var dynamoDbScript = File.CreateText(Path.Combine(outputFolder, "dynamo-db-clean.ps1"));
var dynamoDbSettings = host.Services.GetRequiredService<IOptions<DynamoDbSettings>>();

dynamoDbScript.WriteLine("# Deletions");
foreach (var pk in dynamoDbPksToDelete)
{
    dynamoDbScript.WriteLine($"aws dynamodb delete-item --table-name {dynamoDbSettings.Value.TableName} --key {pk}");
}

dynamoDbScript.WriteLine();
dynamoDbScript.WriteLine("# Anonymisations");

string[] scriptsToCopy = ["expression-attribute-names.json", "expression-attribute-values.json"];
foreach (var scriptToCopy in scriptsToCopy)
{
    File.Copy(Path.Combine(scriptsFolder, scriptToCopy), Path.Combine(outputFolder, scriptToCopy));
}

var columns = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("scripts/expression-attribute-names.json"));

foreach (var pk in dynamoDbPksToAnonymise)
{
    dynamoDbScript.WriteLine($"aws dynamodb update-item --table-name {dynamoDbSettings.Value.TableName} --key {pk} --expression-attribute-names file://expression-attribute-names.json --expression-attribute-values file://expression-attribute-values.json --update-expression \"SET {string.Join(", ", columns.Keys.Select(i => $"{i} = {i.Replace('#', ':')}"))} \"");
}

var cognitoSettings = host.Services.GetRequiredService<IOptions<CognitoSettings>>();
using var cognitoScript = File.CreateText(Path.Combine(outputFolder, "cognito-clean.ps1"));
foreach (var cognitoUserNameToRemove in cognitoUsernamesToRemove)
{
    cognitoScript.WriteLine($"admin-delete-user --user-pool-id {cognitoSettings.Value.UserPoolId} --username {cognitoUserNameToRemove}");
}