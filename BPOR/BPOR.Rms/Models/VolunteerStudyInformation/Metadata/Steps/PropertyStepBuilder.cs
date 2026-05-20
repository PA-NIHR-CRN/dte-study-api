using System.Linq.Expressions;

namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public class PropertyStepBuilder<T> : StepBuilderBase<T>
{
    private readonly List<IPropertyBuilder<T>> _properties = new();
    private string _stepTitle;
    private string _stepDescription;
    private string _postActionName;

    public PropertyStepBuilder(string stepTitle)
    {
        _stepTitle = stepTitle;
    }

    public PropertyStepBuilder<T> WithDescription(string description)
    {
        _stepDescription = description;
        return this;
    }
    
    public PropertyStepBuilder<T> AddProperty<TProp>(Expression<Func<T, TProp>> propertyExpr, Action<SimplePropertyBuilder<T, TProp>>? action = null)
    {
        var builder = new SimplePropertyBuilder<T, TProp>(propertyExpr);
        action?.Invoke(builder);
        _properties.Add(builder);
        return this;
    }
    
    public PropertyStepBuilder<T> AddRadioProperty<TProp>(Expression<Func<T, TProp>> propertyExpr, Action<RadioPropertyBuilder<T, TProp>> action)
    {
        var radioPropertyBuilder = new RadioPropertyBuilder<T, TProp>(propertyExpr);
        action(radioPropertyBuilder);
        _properties.Add(radioPropertyBuilder);
        return this;
    }

    public PropertyStepBuilder<T> AddBooleanRadioProperty(Expression<Func<T, bool>> propertyExpr, string trueCaption,
        string falseCaption)
        => AddRadioProperty(propertyExpr, prop => prop
            .AddOption(true, trueCaption)
            .AddOption(false, falseCaption));
    
    public PropertyStepBuilder<T> AddYesNoProperty(Expression<Func<T, bool>> propertyExpr)
        => AddRadioProperty(propertyExpr, prop => prop
            .AddOption(true, "Yes")
            .AddOption(false, "No"));

    protected override Step<T> Build(StepOptions<T> stepOptions) => new PropertyStep<T>(stepOptions, _properties.Select(i => i.Build()), _stepTitle, _stepDescription, _postActionName);

    public void WithPostAction(string postActionName)
    {
        _postActionName = postActionName;
    }
}