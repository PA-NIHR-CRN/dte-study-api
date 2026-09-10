namespace DeletedUserTool;

public class VerifyAudit(RmsDatabase rmsDatabase)
{
    public void Verify(IEnumerable<EmailAddressAudit> emailAddressAudits)
    {
        using var report = File.CreateText("Verification.txt");
        foreach (var emailAddressAudit in emailAddressAudits)
        {
            var participantCount = rmsDatabase.CountParticipantsByEmail(emailAddressAudit.Email);
            if (participantCount > 0)
            {
                
            }
        }
    }
}