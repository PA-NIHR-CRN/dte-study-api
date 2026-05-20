namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

public interface IPropertyBuilder<T>
{
    IProperty<T> Build();
}