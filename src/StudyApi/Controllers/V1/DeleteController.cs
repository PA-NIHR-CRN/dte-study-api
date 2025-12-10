using System.Threading.Tasks;
using Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace StudyApi.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/delete")]
public class DeleteController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private readonly IParticipantService _participantService;

    public DeleteController(IWebHostEnvironment environment, IParticipantService participantService)
    {
        _environment = environment;
        _participantService = participantService;
    }

    [AllowAnonymous]
    [HttpDelete("participantlocal")]
    public async Task<IActionResult> DeleteParticipantAccount([FromBody] DeleteRequest request)
    {
        if (!_environment.IsDevelopment())
        {
            return Forbid();
        }
        else
        {
            return Ok(_participantService.DeleteUserAsync(request.ParticipantId));
        }
    }

    public class DeleteRequest
    {
        public string ParticipantId { get; set; }
    }
}
