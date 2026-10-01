namespace BPOR.Rms.Models.Study;

public sealed class StudyFilterViewModel
{
    public IReadOnlyCollection<StudyStatusFilterOptionViewModel> StatusOptions { get; init; } = [];

    public bool HasSelectedFilters => StatusOptions.Any(x => x.IsSelected);
}