namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public class Step<T>
{
    public Predicate<T>? ShowOnlyIfCondition { get; }

    public Step(StepOptions<T> stepOptions)
    {
        ShowOnlyIfCondition = stepOptions.ShowOnlyIfCondition;
    }
}