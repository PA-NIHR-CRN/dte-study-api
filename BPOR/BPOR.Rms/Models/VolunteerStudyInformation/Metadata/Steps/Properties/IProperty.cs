using System.Linq.Expressions;

namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public interface IProperty<TModel>
{
    public Expression<Func<TModel, object>> PropertyExpr { get; }
}