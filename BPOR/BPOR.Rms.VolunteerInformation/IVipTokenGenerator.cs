namespace BPOR.Rms.VolunteerInformation;

public interface IVipTokenGenerator
{
    string GenerateToken(VipTokenPurpose purpose, long id, string ivHexString);
    bool TryValidateToken(string token, out (VipTokenPurpose purpose, long Id) result);
    string GenerateIvString();
    string GenerateToken(VipTokenPurpose purpose, long id);
    string GenerateToken(VipTokenPurpose purpose, long id, byte[] iv);
}