using System.Collections.Immutable;

namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class Section<T>
{
    public string Title { get; }
    public ImmutableArray<Step<T>> Steps { get; }

    public Section(string title, IEnumerable<Step<T>> steps)
    {
        Title = title;
        Steps = steps.ToImmutableArray();
    }
}