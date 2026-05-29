using System.Collections.Immutable;

namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class PropertyStep<T> : Step<T>
{
    public string PostActionName { get; }
    public string StepTitle { get; }
    public string StepDescription { get; }
    public ImmutableArray<IProperty<T>> Properties { get; }

    public PropertyStep(StepOptions<T> stepOptions, IEnumerable<IProperty<T>> properties, string stepTitle,
        string stepDescription, string postActionName)
        : base(stepOptions)
    {
        PostActionName = postActionName;
        StepTitle = stepTitle;
        StepDescription = stepDescription;
        Properties = properties.ToImmutableArray();
    }
}