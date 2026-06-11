using System.Linq.Expressions;

namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class PropertyOptions<T, TProp>
{
    public string Caption { get; set; }
    public Expression<Func<T, TProp>> PropExpr { get; set; }
}