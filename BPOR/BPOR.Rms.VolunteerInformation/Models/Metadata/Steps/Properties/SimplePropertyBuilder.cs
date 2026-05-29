using System.Linq.Expressions;

namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class SimplePropertyBuilder<TModel, TProperty> : PropertyBuilder<TModel, TProperty>
{
    private string _caption;

    public SimplePropertyBuilder(Expression<Func<TModel, TProperty>> propExpr) : base(propExpr)
    {
    }

    public override Property<TModel, TProperty> Build(PropertyOptions<TModel, TProperty> options) => new SimpleProperty<TModel, TProperty>(options);
}