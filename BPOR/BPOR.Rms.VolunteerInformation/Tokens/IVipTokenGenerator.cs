namespace BPOR.Rms.VolunteerInformation.Tokens;

public interface IVipTokenGenerator
{
    string GenerateToken(VipToken token);
    string GenerateToken(VipTokenPurpose purpose, long id);

    string GenerateVolunteerToken();
    Task<VipToken?> ValidateToken(string tokenString, CancellationToken cancellationToken);
}