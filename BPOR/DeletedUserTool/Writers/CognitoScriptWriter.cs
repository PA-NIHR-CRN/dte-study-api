using Microsoft.Extensions.Options;

namespace DeletedUserTool.Writers;

public class CognitoScriptWriter(IOptions<CognitoSettings> settings, string outputFolderPath)
    : PowershellScriptWriter(File.CreateText(Path.Combine(outputFolderPath, "cognito-clean.ps1")))
{
    public void WriteDeleteUser(string userName)
    {
       TextWriter.WriteLine($"aws cognito-idp admin-delete-user --profile {settings.Value.Profile} --user-pool-id {settings.Value.UserPoolId} --username {userName}");
    }
}
