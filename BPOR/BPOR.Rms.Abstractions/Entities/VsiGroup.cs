namespace BPOR.Rms.Abstractions.Entities;

public class VsiGroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<VsiGroupCriterion> Criteria { get; set; } = new();
}