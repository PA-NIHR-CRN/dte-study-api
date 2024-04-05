using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudiesViewModel
{
    public IEnumerable<StudyModel> Studies { get; set; }
    public string SearchTerm { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasSearched { get; set; }
    
}

