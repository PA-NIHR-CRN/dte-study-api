namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public class CustomViewStepBuilder<T> : StepBuilderBase<T>
{
    public string ViewPath { get; }

    public CustomViewStepBuilder(string viewPath)
    {
        ViewPath = viewPath;
    }

    protected override Step<T> Build(StepOptions<T> options) => new CustomViewStep<T>(options, ViewPath);
}