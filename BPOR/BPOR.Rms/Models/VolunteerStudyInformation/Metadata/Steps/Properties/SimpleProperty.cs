using System.Linq.Expressions;

namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public class SimpleProperty<TModel, TProperty> : Property<TModel, TProperty>
{
    public SimpleProperty(PropertyOptions<TModel, TProperty> options)
        : base(options)
    {
    }
}