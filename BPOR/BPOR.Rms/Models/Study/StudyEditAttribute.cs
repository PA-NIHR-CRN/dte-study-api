namespace BPOR.Rms.Models.Study;

public class StudyEditAttribute : Attribute
{
    public StudyEditAttribute(int fieldId)
    {
        FieldId = fieldId;
    }

    public int FieldId { get; }
}
