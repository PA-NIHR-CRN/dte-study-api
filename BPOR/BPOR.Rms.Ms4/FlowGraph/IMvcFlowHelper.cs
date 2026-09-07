using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Ms4.FlowGraph;

public interface IMvcFlowHelper
{
    MvcActionKey CurrentActionKey { get; }
    IUrlHelper UrlHelper { get; }
}