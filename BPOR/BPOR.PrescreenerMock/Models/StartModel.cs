using BPOR.Rms.Abstractions.Models;

namespace BPOR.PrescreenerMock.Models;

public class StartModel
{
    public GetInformationResponse Info { get; }
    public string Id { get; }

    public StartModel(GetInformationResponse info, string id)
    {
        Info = info;
        Id = id;
    }
}