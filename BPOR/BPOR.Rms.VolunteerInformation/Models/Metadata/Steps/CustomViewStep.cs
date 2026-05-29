namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class CustomViewStep<T> : Step<T>
{
    public string ViewPath { get; }

    public CustomViewStep(StepOptions<T> options, string viewPath) :
        base(options)
    {
        ViewPath = viewPath;
    }
}