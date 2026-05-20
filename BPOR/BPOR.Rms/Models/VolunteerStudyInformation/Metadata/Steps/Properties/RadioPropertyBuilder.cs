using System.Linq.Expressions;

namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public class RadioPropertyBuilder<TModel, TProperty> : PropertyBuilder<TModel, TProperty>
{
    private List<MultiSelectOption<TProperty>> _radioOptions = new();
    public RadioPropertyBuilder(Expression<Func<TModel, TProperty>> propExpr) : base(propExpr)
    {
    }
    
    public override Property<TModel, TProperty> Build(PropertyOptions<TModel, TProperty> options) => new RadioProperty<TModel, TProperty>(options, _radioOptions);

    public RadioPropertyBuilder<TModel, TProperty> AddOption(TProperty value, string caption)
    {
        _radioOptions.Add(new MultiSelectOption<TProperty>(value, caption));
        return this;
    }
}