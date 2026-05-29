using System.Linq.Expressions;

namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public interface IStepBuilder<T>
{
    IStepBuilder<T> WithProperty(Expression<Func<T, object?>> propertyExpr);
}