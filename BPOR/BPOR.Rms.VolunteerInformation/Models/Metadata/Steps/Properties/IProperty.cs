using System.Linq.Expressions;

namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public interface IProperty<TModel>
{
    public Expression<Func<TModel, object>> PropertyExpr { get; }
}