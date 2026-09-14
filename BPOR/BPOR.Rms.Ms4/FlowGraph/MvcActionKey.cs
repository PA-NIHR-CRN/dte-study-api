using JetBrains.Annotations;

namespace BPOR.Rms.Ms4.FlowGraph;

public record MvcActionKey([AspMvcController] string Controller, [AspMvcAction] string Action)
{
    public override string ToString()
    {
        return $"{Controller}.{Action}";
    }

    public static MvcActionKey Parse(string value)
    {
        var parts = value.Split('.', 2);
        return new  MvcActionKey(parts[0], parts[1]);
    }
}