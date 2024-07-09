using NIHR.Infrastructure.Paging;

namespace BPOR.Rms.Models.Study;

public class StudiesViewModel
{
    public Page<StudyModel> Studies { get; set; } = Page<StudyModel>.Empty();
    public string? SearchTerm { get; set; }
    public bool HasSearched { get; set; }
    public bool HasBeenReset { get; set; }
}
