using System.Linq.Expressions;

namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public interface IStepBuilder<T>
{
    IStepBuilder<T> WithProperty(Expression<Func<T, object?>> propertyExpr);
}