namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class SectionBuilder<T>
{
    private readonly string _sectionTitle;
    private List<StepBuilderBase<T>> _steps = new();

    public SectionBuilder(string sectionTitle)
    {
        _sectionTitle = sectionTitle;
        throw new NotImplementedException();
    }

    public SectionBuilder<T> AddPropertyStep(string title, Action<PropertyStepBuilder<T>>? action = null)
    {
        var step = new PropertyStepBuilder<T>(title);
        action?.Invoke(step);
        _steps.Add(step);
        return this;
    }

    public SectionBuilder<T> AddCustomActionStep(string viewPath, Action<CustomViewStepBuilder<T>>? action = null)
    {
        CustomViewStepBuilder<T> stepBuilder = new CustomViewStepBuilder<T>(viewPath);
        action?.Invoke(stepBuilder);
        _steps.Add(stepBuilder);
        return this;
    }

    public Section<T> Build() => new(_sectionTitle, _steps.Select(step => step.Build()));
    
}