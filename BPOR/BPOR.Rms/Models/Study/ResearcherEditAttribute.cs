namespace BPOR.Rms.Models.Study;

public class ResearcherEditAttribute : Attribute
{
    public ResearcherEditAttribute(int fieldId)
    {
        FieldId = fieldId;
    }

    public int FieldId { get; }
}
