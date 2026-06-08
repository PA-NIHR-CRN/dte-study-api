using System.Linq.Expressions;

namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public abstract class PropertyBuilder<T, TProp> : IPropertyBuilder<T>
{
    private PropertyOptions<T, TProp> _options = new();

    public PropertyBuilder(Expression<Func<T, TProp>> propExpr)
    {
        _options.PropExpr = propExpr;
    }

    public abstract IProperty<T> Build(PropertyOptions<T, TProp> options);
    
    public void WithCaption(string caption)
    {
        _options.Caption = caption;
    }

    public IProperty<T> Build() => Build(_options);
}