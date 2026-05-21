namespace BPOR.Rms.Api;

public interface IRrvTokenGenerator
{
    string GenerateToken(long campaignParticipantId);
    bool TryValidateToken(string token, out long campaignParticipantId);
}