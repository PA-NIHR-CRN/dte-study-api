using ProtoBuf;

namespace BPOR.Rms.VolunteerInformation.Tokens;

[ProtoContract]
public class VipToken
{
    public VipToken()
    {
    }

    public VipToken(VipTokenPurpose purpose, int campaignId, int participantId, int studyId)
    {
        Purpose = purpose;
        CampaignId = campaignId;
        ParticipantId = participantId;
        StudyId = studyId;
    }

    [ProtoMember(1)]
    public VipTokenPurpose Purpose { get; set; }
    [ProtoMember(2)]
    public int CampaignId { get; set; }
    [ProtoMember(3)]
    public int ParticipantId { get; set; }
    [ProtoMember(4)]
    public int StudyId { get; set; }
}