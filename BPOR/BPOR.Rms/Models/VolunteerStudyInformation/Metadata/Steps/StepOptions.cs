namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public record StepOptions<T>(Predicate<T> ShowOnlyIfCondition);