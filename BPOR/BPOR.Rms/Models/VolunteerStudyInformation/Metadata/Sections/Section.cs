using System.Collections.Immutable;

namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

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