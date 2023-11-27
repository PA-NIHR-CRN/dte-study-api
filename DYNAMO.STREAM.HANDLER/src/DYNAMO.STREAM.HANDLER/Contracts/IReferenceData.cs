namespace DYNAMO.STREAM.HANDLER.Contracts;

public interface IReferenceData
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
}
