namespace BPOR.Rms.VolunteerInformation.Tokens;

public interface IVipTokenGenerator
{
    string GenerateToken(VipToken token);

    Task<VipToken?> ValidateToken(string tokenString, CancellationToken cancellationToken);
}