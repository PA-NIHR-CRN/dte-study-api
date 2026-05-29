namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public record StepOptions<T>(Predicate<T> ShowOnlyIfCondition);