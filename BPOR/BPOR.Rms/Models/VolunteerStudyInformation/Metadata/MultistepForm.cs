using System.Collections.Immutable;

namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public class MultistepForm<T>
{
    public MultistepForm(IEnumerable<Section<T>> sections)
    {
        Sections = sections.ToImmutableArray();
    }

    public ImmutableArray<Section<T>> Sections { get; }
}