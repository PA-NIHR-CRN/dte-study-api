namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public interface IPropertyBuilder<T>
{
    IProperty<T> Build();
}