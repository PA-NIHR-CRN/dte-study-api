namespace BPOR.Rms.VolunteerInformation;

public interface IRrvTokenGenerator
{
    string GenerateToken(long campaignParticipantId);
    bool TryValidateToken(string token, out long campaignParticipantId);
}