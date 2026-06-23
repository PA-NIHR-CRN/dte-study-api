using ProtoBuf;

namespace BPOR.Rms.VolunteerInformation.Tokens;

[ProtoContract]
public class VipToken
{
    public VipToken()
    {
    }

    public VipToken(VipTokenPurpose purpose, long id)
    {
        Purpose = purpose;
        Id = id;
    }

    [ProtoMember(1)]
    public VipTokenPurpose Purpose { get; set; }
    [ProtoMember(2)]
    public long Id { get; set; }
}