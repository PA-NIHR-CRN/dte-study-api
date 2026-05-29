namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class SimpleProperty<TModel, TProperty> : Property<TModel, TProperty>
{
    public SimpleProperty(PropertyOptions<TModel, TProperty> options)
        : base(options)
    {
    }
}