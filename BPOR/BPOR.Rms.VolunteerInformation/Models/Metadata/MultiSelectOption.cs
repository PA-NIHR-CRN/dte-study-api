namespace BPOR.Rms.VolunteerInformation.Models.Metadata;

public class MultiSelectOption<TValue>
{
    public TValue Value { get; }
    public string Caption { get; }

    public MultiSelectOption(TValue value, string caption)
    {
        Value = value;
        Caption = caption;
    }
}