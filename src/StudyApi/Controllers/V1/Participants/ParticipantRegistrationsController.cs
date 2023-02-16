using System.Threading.Tasks;
using Application.Participants.V1.Commands.ParticipantRegistrations;
using Application.Participants.V1.Queries.ParticipantRegistrations;
using Domain.Entities.ParticipantRegistrations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Attributes;

namespace StudyApi.Controllers.V1.Participants
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/studies")]
    [AuthorizeResearcher]
    public class ParticipantRegistrationsController : Controller
    {
        private readonly IMediator _mediator;

        public ParticipantRegistrationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet("{studyId:long}/sites/{siteId}/participants")]
        //public async Task<IActionResult> GetParticipantRegistrationsByStudySite(long studyId, string siteId)
        //{
        //    return Ok(await _mediator.Send(new GetParticipantRegistrationsByStudySiteQuery(studyId, siteId)));
        //}
        
        //[HttpGet("{studyId:long}/participants/{participantId}")]
        //public async Task<IActionResult> GetParticipantsRegistrationsByStudy(long studyId, string participantId)
        //{
        //    return Ok(await _mediator.Send(new GetParticipantsRegistrationByStudyQuery(studyId, participantId)));
        //}
        
        //[HttpGet("{studyId:long}/sites/{siteId}/participants/{participantId}")]
        //public async Task<IActionResult> GetParticipantRegistrationByStudySite(long studyId, string siteId, string participantId)
        //{
        //    return Ok(await _mediator.Send(new GetParticipantRegistrationByStudySiteQuery(studyId, siteId, participantId)));
        //}
        
        //[HttpGet("{studyId:long}/sites/{siteId}/participantstatus/{status}")]
        //public async Task<IActionResult> GetParticipantsRegistrationByStudySiteStatus(long studyId, string siteId, ParticipantRegistrationStatus status)
        //{
        //    return Ok(await _mediator.Send(new GetParticipantRegistrationsByStudySiteStatusQuery(studyId, siteId, status)));
        //}
        
        //[HttpGet("{studyId:long}/participants/status/{status}")]
        //public async Task<IActionResult> GetParticipantRegistrationsStatusByStudy(long studyId, ParticipantRegistrationStatus status)
        //{
        //    return Ok(await _mediator.Send(new GetParticipantRegistrationsStatusByStudyQuery(studyId, status)));
        //}

        //[HttpPost("{studyId:long}/sites/{siteId}/participants/{participantId}/screening")]
        //public async Task<IActionResult> SetScreeningParticipantRegistration(long studyId, string siteId, string participantId)
        //{
        //    var command = new SetScreeningParticipantRegistrationCommand(studyId, siteId, participantId);
            
        //    await _mediator.Send(command);
                
        //    return Ok();
        //}
        
        //[HttpPost("{studyId:long}/sites/{siteId}/participants/{participantId}/notselected")]
        //public async Task<IActionResult> SetNotSelectedParticipantRegistration(long studyId, string siteId, string participantId)
        //{
        //    var command = new SetNotSelectedParticipantRegistrationCommand(studyId, siteId, participantId);
            
        //    await _mediator.Send(command);
                
        //    return Ok();
        //}
    }
}