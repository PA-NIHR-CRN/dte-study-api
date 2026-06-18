using BPOR.PrescreenerMock.Models;
using BPOR.Rms.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.PrescreenerMock.Controllers;

[Route("Recruit-Registry-Volunteers")]
public class RecruitRegistryVolunteersController(
    IRmsApiClient apiClient, ILogger<RecruitRegistryVolunteersController> logger): Controller
{
    [HttpGet("volunteer/check-research")]
    public async Task<IActionResult> Start(string token, string id, CancellationToken cancellationToken)
    {
        var info = await apiClient.GetInformation(token, cancellationToken);
        return View(new StartModel(info, id));
    }
}