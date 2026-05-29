namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public abstract class StepBuilderBase<T>
{
    protected Predicate<T> ShowOnlyIfCondition { get; private set; }

    public StepBuilderBase<T> ShowOnlyIf(Predicate<T> condition)
    {
        ShowOnlyIfCondition = condition;
        return this;
    }

    public Step<T> Build() => Build(new StepOptions<T>(ShowOnlyIfCondition));

    protected abstract Step<T> Build(StepOptions<T> stepOptions);
}