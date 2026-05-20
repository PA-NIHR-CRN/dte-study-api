using BPOR.Domain.Enums;

namespace BPOR.Rms.Models.Study.VolunteerInformation;

public class VsiEditModel
{
    public string StepName { get; }
    public long Id { get; }
    public int Step { get; }
}

public class Step1Model : VsiEditModel
{
    public string Description { get; set; }
}

public class Step2Model : VsiEditModel
{
    public VsiStudyTypeId StudyType { get; set; }
}