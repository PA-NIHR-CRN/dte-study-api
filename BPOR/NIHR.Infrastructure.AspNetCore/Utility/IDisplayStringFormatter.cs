namespace NIHR.Infrastructure.AspNetCore;

public interface IDisplayStringFormatter
{
    public string ToDisplayString(object? value);
}