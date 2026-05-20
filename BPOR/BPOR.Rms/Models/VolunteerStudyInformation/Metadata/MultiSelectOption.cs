namespace BPOR.Rms.Models.VolunteerStudyInformation.Metadata;

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