namespace BPOR.Rms.VolunteerInformation.Models;

public interface IContextProvider<out T>
{
    T Context { get; }
}