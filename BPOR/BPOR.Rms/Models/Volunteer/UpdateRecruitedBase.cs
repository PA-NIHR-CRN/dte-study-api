using BPOR.Domain.Entities;
using UserRole = BPOR.Domain.Enums.UserRole;

namespace BPOR.Rms.Models.Volunteer;

public abstract class UpdateRecruitedBase
{
    public string? StudyName { get; set; }
    public int StudyId { get; set; }
    public bool HasCampaigns { get; set; }
    
    /// <summary>
    /// Determines whether the given user has permissions to update recruitment details for this study.
    /// </summary>
    public bool CanUpdateRecruitment(User? user)
    {
        if (user == null) return false;
        
        return (HasCampaigns && user.HasRole(UserRole.Researcher)) 
               || user.HasRole(UserRole.Admin);
    }
}