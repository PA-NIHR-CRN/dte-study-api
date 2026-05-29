namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class Step<T>
{
    public Predicate<T>? ShowOnlyIfCondition { get; }

    public Step(StepOptions<T> stepOptions)
    {
        ShowOnlyIfCondition = stepOptions.ShowOnlyIfCondition;
    }
}