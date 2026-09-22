using System.Text.Json;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using DeletedUserTool;
using DeletedUserTool.Writers;
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
var emailAddressesToBeRemoved = rmsDatabase.GetDeletedParticipantEmails().Distinct().ToArray();

var bporCognito = host.Services.GetRequiredService<BporCognito>();
var bporDynamoDb = host.Services.GetRequiredService<BporDynamoDb>();

List<DynamoParticipant> dynamoParticipantsByEmail = await bporDynamoDb.GetParticipantsByEmail(
    emailAddressesToBeRemoved);

string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"output-{DateTime.UtcNow:yyyy-MM-ddTHH.mm.ss}");
string scriptsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");
Directory.CreateDirectory(outputFolder);

using var cognitoScriptWriter = new CognitoScriptWriter(host.Services.GetRequiredService<IOptions<CognitoSettings>>(), outputFolder);
using var dynamoDbScriptWriter = new DynamoDbScriptWriter(host.Services.GetRequiredService<IOptions<DynamoDbSettings>>(), outputFolder);
using var rmsDbScriptWriter = new RmsDbScriptWriter(outputFolder);

List<EmailAddressAudit> audits = new();

foreach (var emailAddress in emailAddressesToBeRemoved)
{
    string anonEmail = AnonEmail(emailAddress);
    dynamoDbScriptWriter.BeginSection($"Email: {anonEmail}");
    rmsDbScriptWriter.BeginSection($"Email: {anonEmail}");
    cognitoScriptWriter.BeginSection($"Email: {anonEmail}");
    
    // dynamoDbScriptWriter.BeginSection($"Email: {emailAddress}");
    // rmsDbScriptWriter.BeginSection($"Email: {emailAddress}");
    // cognitoScriptWriter.BeginSection($"Email: {emailAddress}");

    Console.WriteLine($"Found deleted RMS participant {anonEmail}");
    EmailAddressAudit emailAddressAudit = new() { Email = emailAddress };
    audits.Add(emailAddressAudit);

    var cognitoUsersMatchingEmail = await bporCognito.GetUserByEmail(emailAddress).ToArrayAsync();
    emailAddressAudit.CognitoUserIdsMatchingEmail = cognitoUsersMatchingEmail.Select(i => i.Username).ToArray();
    foreach (var cognitoUser in cognitoUsersMatchingEmail)
    {
        cognitoScriptWriter.WriteDeleteUser(cognitoUser.Username);
        Console.WriteLine($"    with Cognito User {cognitoUser.Username}");
    }

    var dynamoParticipantsMatchingEmail = dynamoParticipantsByEmail.Where(i => i.Email == emailAddress).ToArray();
    emailAddressAudit.DynamoDbRecordsMatchingEmail.AddRange(dynamoParticipantsMatchingEmail.Select(i => new DynamoDbAudit(i)));
    foreach (var dynamoParticipant in dynamoParticipantsMatchingEmail)
    {
        if (dynamoParticipant.Pk.StartsWith("PARTICIPANT#"))
        {
            dynamoDbScriptWriter.WriteDynamoDbDelete(dynamoParticipant);
        }
        else if (dynamoParticipant.Pk.StartsWith("DELETED#"))
        {
            dynamoDbScriptWriter.WriteDynamoDbAnonymise(dynamoParticipant);
        }
        Console.WriteLine(
            $"  with DynamoDB record by email {FormatRecord(dynamoParticipant)}");
    }

    var rmsParticipants = rmsDatabase.GetParticipantsByEmail(emailAddress);
    // Get all RMS participant records for the email address to be removed
    foreach (var rmsParticipant in rmsParticipants)
    {
        rmsDbScriptWriter.BeginSection($"Participant ID: {rmsParticipant.Id}");
        var rmsParticipantAudit = new RmsParticipantAudit(){Id = rmsParticipant.Id};
        emailAddressAudit.RmsParticipants.Add(rmsParticipantAudit);
        
        Console.WriteLine($"  with RMS participant ID {rmsParticipant.Id}");
        
        // Get all participant identifiers for the participant
        var participantsIdentifiers = rmsDatabase.GetParticipantsIdentifiers(rmsParticipant.Id);
        
        if (participantsIdentifiers.Any(i => i.IdentifierTypeId == IdentifierTypes.Deleted))
        {
            rmsDbScriptWriter.WriteAnonymiseParticipant(rmsParticipant.Id);
        }
        else
        {
            rmsDbScriptWriter.WriteDeleteParticipant(rmsParticipant.Id);
        }
        
        foreach (var participantIdentifier in participantsIdentifiers)
        {
            rmsDbScriptWriter.BeginSection($"Participant Identifier: {participantIdentifier.Id} {participantIdentifier.IdentifierTypeId} {participantIdentifier.Value}");
            RmsParticipantIdentifierAudit rmsParticipantIdentifierAudit = new()
            {
                Id = participantIdentifier.Id,
                IdentifierTypeId = participantIdentifier.IdentifierTypeId, 
                Value = participantIdentifier.Value
            };
            rmsParticipantAudit.ParticipantIdentifierAudits.Add(rmsParticipantIdentifierAudit);
            string pkPrefix;
            var identifierTypeId = participantIdentifier.IdentifierTypeId;
            if (!TryGetPkPrefix(identifierTypeId, out pkPrefix))
            {
                continue;
            }

            Console.WriteLine(
                $"    with Participant Identifier {participantIdentifier.IdentifierTypeId} {participantIdentifier.Value}");

            var pk = $"{pkPrefix}#{participantIdentifier.Value}";
            var sk = $"{pkPrefix}#";
            DynamoParticipant? dynamoRecord = await bporDynamoDb.GetParticipantByKey(pk, sk);
            if (dynamoRecord != null)
            {
                switch (participantIdentifier.IdentifierTypeId)
                {
                    case IdentifierTypes.ParticipantId:
                    case IdentifierTypes.NhsId:
                        dynamoDbScriptWriter.WriteDynamoDbDelete(dynamoRecord);
                        break;
                    case IdentifierTypes.Deleted:
                        dynamoDbScriptWriter.WriteDynamoDbAnonymise(dynamoRecord);
                        break;
                }
                Console.WriteLine(
                    $"      with DynamoDB record by PK {FormatRecord(dynamoRecord)}");
                rmsParticipantIdentifierAudit.DynamoDbRecords.Add(new(dynamoRecord));
            }
            
            // Generate SQL to clean up the RMS records in case the stream handler does not make all the required updates
            if (participantIdentifier.IdentifierTypeId is IdentifierTypes.ParticipantId or IdentifierTypes.NhsId)
            {
                rmsDbScriptWriter.WriteDeleteParticpantIdentifer(participantIdentifier.Id);
            }

            rmsDbScriptWriter.EndSection();
        }
        
        rmsDbScriptWriter.EndSection();
    }
    
    cognitoScriptWriter.EndSection();
    dynamoDbScriptWriter.EndSection();
    rmsDbScriptWriter.EndSection();
}

static string FormatRecord(DynamoParticipant dynamoParticipant) =>
    $"PK:{dynamoParticipant.Pk} SK:{dynamoParticipant.Sk} Email:{dynamoParticipant.Email} NhsNo:{dynamoParticipant.NhsNumber} NhsId:{dynamoParticipant.NhsId}";

File.WriteAllText(Path.Combine(outputFolder, "audit.json"), JsonSerializer.Serialize(audits));

string[] scriptsToCopy = ["expression-attribute-names.json"];
foreach (var scriptToCopy in scriptsToCopy)
{
    File.Copy(Path.Combine(scriptsFolder, scriptToCopy), Path.Combine(outputFolder, scriptToCopy));
}

bool TryGetPkPrefix(IdentifierTypes identifierTypes, out string s)
{
    switch (identifierTypes)
    {
        case IdentifierTypes.Deleted:
            s = "DELETED";
            break;
        case IdentifierTypes.ParticipantId:
            s = "PARTICIPANT";
            break;
        case IdentifierTypes.NhsId:
            s = "PARTICIPANT";
            break;
        default:
            s = null;
            return false;
    }

    return true;
}

string AnonEmail(string emailAddress1)
{
    var parts = emailAddress1.Split('@', 2);
    if (parts.Length == 2)
    {
        return $"{parts[0].Substring(0, 2)}***@{parts[1].Substring(0, 2)}***.***";
    }
    else
    {
        return $"{emailAddress1.Substring(0, 3)}@***.***";
    }
}