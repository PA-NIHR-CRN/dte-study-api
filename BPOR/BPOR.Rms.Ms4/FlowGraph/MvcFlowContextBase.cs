using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.Ms4.FlowGraph;

public record MvcFlowContextBase
{   
    public Uri? ReturnUrl { get; set; }
}