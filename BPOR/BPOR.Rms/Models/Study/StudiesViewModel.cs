using NIHR.Infrastructure.Paging;

namespace BPOR.Rms.Models.Study;

public class StudiesViewModel
{
    public Page<StudyModel> Studies { get; set; } = Page<StudyModel>.Empty();
    public string? SearchTerm { get; set; }
    public bool IsSearched { get; set; }
    public bool IsFiltered { get; set; }
    public StudyFilterViewModel Filters { get; init; } = new();
}
