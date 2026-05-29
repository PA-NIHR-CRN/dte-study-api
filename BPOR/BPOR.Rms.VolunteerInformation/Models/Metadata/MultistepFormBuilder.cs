namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class MultistepFormBuilder<T>
{
    private readonly List<SectionBuilder<T>> _sections = new List<SectionBuilder<T>>();

    public MultistepFormBuilder<T> AddSection(string sectionTitle, Action<SectionBuilder<T>> action)
    {
        var section = new SectionBuilder<T>(sectionTitle);
        action(section);
        _sections.Add(section);
        return this;
    }
    
    public MultistepForm<T> Build() => new MultistepForm<T>(_sections.Select(step => step.Build()));
}