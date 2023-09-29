using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using Domain.Entities.Participants;
using Dte.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1.Local;

[ApiController]
[ApiVersion("1")]
[Route("api/local")]
public class LocalController : Controller
{
    private readonly IParticipantService _participantService;
    private readonly IParticipantRepository _participantRepository;
    private readonly IUserService _userService;
    private readonly ILogger<LocalController> _logger;

    public LocalController(IParticipantService participantService, IParticipantRepository participantRepository,
        IUserService userService, ILogger<LocalController> logger)
    {
        _participantService = participantService;
        _participantRepository = participantRepository;
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("UpdateLinkedRecords")]
    public async Task<IActionResult> UpdateLinkedRecords(CancellationToken cancellationToken = default)
    {
        var userRecordErrors = new List<UserRecordError>();
        await foreach (var userRecord in _participantRepository)
        {
            if (userRecord.Pk.Contains("DELETED#"))
            {
                continue;
            }

            // get a record that has a NhsNumber and a Participant ID, I will therefore know this record in the db has another record that is linked
            if (userRecord.NhsNumber != null && userRecord.ParticipantId != null)
            {
                try
                {
                    // check and see if the cognito record for this user has been disabled to confirm this is a linked account
                    var cognitoRecord = await _userService.AdminGetUserAsync(userRecord.ParticipantId, cancellationToken);
                    if (!cognitoRecord.Enabled)
                    {
                        // get the linked account row by passing the ParticipantId into the pk as the new row saves the PK as ParticipantId to link 
                        var linkedRecord =
                            await _participantRepository.GetParticipantAsync(
                                $"PARTICIPANT#{userRecord.ParticipantId}", cancellationToken);

                        // delete the linked row
                        await _participantRepository.DeleteParticipantAsync(linkedRecord, cancellationToken);

                        // create a new row with all the same info with LINKED# replacing PARTICIPANT#
                        linkedRecord.Pk = $"LINKED#{linkedRecord.ParticipantId}";
                        linkedRecord.Sk = "LINKED#";
                        await _participantRepository.CreateParticipantAsync(linkedRecord, cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    // store errors
                    userRecordErrors.Add(new UserRecordError
                    {
                        UserRecord = userRecord,
                        Error = e.Message,
                        Exception = e
                    });
                }
            }
        }

        // return and write errors for investigation
        _logger.LogError(userRecordErrors.ToString());
        return userRecordErrors.Any()
            ? StatusCode(500, userRecordErrors)
            : Ok(new { Success = true, Message = "Linked records updated successfully." });
    }

    private class UserRecordError
    {
        public Participant UserRecord { get; set; }
        public string Error { get; set; }
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("UpdateDeletedRecords")]
    public async Task<IActionResult> UpdateDeletedRecords(CancellationToken cancellationToken = default)
    {
        var userRecordErrors = new List<UserRecordError>();
        await foreach (var userRecord in _participantRepository)
        {
            try
            {
                if (userRecord.Pk.Contains("DELETED#"))
                {
                    continue;
                }

                if (!userRecord.ConsentRegistration)
                {
                    await _participantService.DeleteUserAsync(userRecord.ParticipantId, cancellationToken);
                }
            }
            catch (Exception e)
            {
                userRecordErrors.Add(new UserRecordError
                {
                    UserRecord = userRecord,
                    Error = e.Message
                });
            }
        }

        // return and write errors for investigation
        _logger.LogError(userRecordErrors.ToString());
        return userRecordErrors.Any()
            ? StatusCode(500, userRecordErrors)
            : Ok(new { Success = true, Message = "Linked records updated successfully." });
    }
}