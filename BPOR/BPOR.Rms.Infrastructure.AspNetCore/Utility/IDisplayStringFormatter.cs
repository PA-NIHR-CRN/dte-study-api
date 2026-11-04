namespace BPOR.Rms.Infrastructure.AspNetCore;

public interface IDisplayStringFormatter
{
    public string ToDisplayString(object? value);
}