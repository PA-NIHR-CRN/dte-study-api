using System.Linq.Expressions;

namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public abstract class Property<TModel, TProperty> : IProperty<TModel>
{
    private readonly Expression<Func<TModel, object>> _untypedPropertyExpression;
    public string Caption { get; }
    public Expression<Func<TModel, TProperty>> PropertyExpr { get; }

    Expression<Func<TModel, object>> IProperty<TModel>.PropertyExpr => _untypedPropertyExpression;

    protected Property(PropertyOptions<TModel, TProperty> options)
    {
        Caption = options.Caption;
        PropertyExpr = options.PropExpr;
        _untypedPropertyExpression = ToUntypedPropertyExpression(options.PropExpr);
    }


    protected static Expression<Func<TInput, object>> ToUntypedPropertyExpression<TInput, TOutput> (Expression<Func<TInput, TOutput>> expression)
    {
        var memberName = ((MemberExpression)expression.Body).Member.Name;

        var param = Expression.Parameter(typeof(TInput));
        var field = Expression.Property(param, memberName);
        return Expression.Lambda<Func<TInput, object>>(field, param);
    }
}