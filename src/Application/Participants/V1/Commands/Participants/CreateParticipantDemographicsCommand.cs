using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Constants;
using Application.Contracts;
using Application.Content;
using Application.Mappings.Participants;
using Application.Models.Participants;
using Application.Settings;
using Domain.Entities.Participants;
using Dte.Common.Contracts;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Participants.V1.Commands.Participants
{
    public class CreateParticipantDemographicsCommand : IRequest<Response<object>>
    {
        public string ParticipantId { get; }
        public string MobileNumber { get; }
        public string LandlineNumber { get; }
        public ParticipantAddressModel Address { get; }
        public string SexRegisteredAtBirth { get; }
        public bool? GenderIsSameAsSexRegisteredAtBirth { get; }
        public string EthnicGroup { get; }
        public string EthnicBackground { get; }
        public bool? Disability { get; }
        public string DisabilityDescription { get; }
        public IEnumerable<string> HealthConditionInterests { get; }

        public CreateParticipantDemographicsCommand(string participantId,
            string mobileNumber,
            string landlineNumber,
            ParticipantAddressModel address,
            string sexRegisteredAtBirth,
            bool? genderIsSameAsSexRegisteredAtBirth,
            string ethnicGroup,
            string ethnicBackground,
            bool? disability,
            string disabilityDescription,
            IEnumerable<string> healthConditionInterests)
        {
            ParticipantId = participantId;
            MobileNumber = mobileNumber;
            LandlineNumber = landlineNumber;
            Address = address;
            SexRegisteredAtBirth = sexRegisteredAtBirth;
            GenderIsSameAsSexRegisteredAtBirth = genderIsSameAsSexRegisteredAtBirth;
            EthnicGroup = ethnicGroup;
            EthnicBackground = ethnicBackground;
            Disability = disability;
            DisabilityDescription = disabilityDescription;
            HealthConditionInterests = healthConditionInterests;
        }

        public class
            CreateParticipantDemographicsCommandHandler : IRequestHandler<CreateParticipantDemographicsCommand,
                Response<object>>
        {
            private readonly IParticipantRepository _participantRepository;
            private readonly IClock _clock;
            private readonly IHeaderService _headerService;
            private readonly ILogger<CreateParticipantDemographicsCommandHandler> _logger;
            private readonly IEmailService _emailService;
            private readonly EmailSettings _emailSettings;

            public CreateParticipantDemographicsCommandHandler(IParticipantRepository participantRepository,
                IClock clock, IHeaderService headerService, ILogger<CreateParticipantDemographicsCommandHandler> logger,
                EmailSettings emailSettings,IEmailService emailService)
            {
                _participantRepository = participantRepository;
                _clock = clock;
                _headerService = headerService;
                _logger = logger;
                _emailService = emailService;
                _emailSettings = emailSettings;
            }

            public async Task<Response<object>> Handle(CreateParticipantDemographicsCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var updateExisting = false;
                   
                    var entity = await _participantRepository.GetParticipantDemographicsAsync(request.ParticipantId);

                    if(!entity.HasDemographics)
                    {
                        var user = await _participantRepository.GetParticipantDetailsAsync(request.ParticipantId);
                        if (user.NhsId is null)
                        {
                            var baseUrl = _emailSettings.WebAppBaseUrl;
                            var htmlBody = EmailTemplate.GetHtmlTemplate().Replace("###TITLE_REPLACE1###",
                                    "New Be Part of Research Account")
                                .Replace("###TEXT_REPLACE1###",
                                    "Thank you for registering for Be Part of Research.")
                                .Replace("###TEXT_REPLACE2###",
                                    $"By signing up, you are joining our community of amazing volunteers who are helping researchers to understand more about health and care conditions. Please visit the <a href=\"https://bepartofresearch.nihr.ac.uk/taking-part/how-to-take-part\">How to take part</a> section of the website to find out about other ways to take part in health and care research.")
                                 .Replace("###TEXT_REPLACE3###",
                                    $"Sign up to our <a href=\"https://nihr.us14.list-manage.com/subscribe?u=299dc02111e8a68172029095f&id=3b030a1027\">newsletter</a> to receive all our research news, studies you can take part in and other opportunities helping to shape health and care research from across the UK.")
                                .Replace("###TEXT_REPLACE4###",
                                    "")
                                .Replace("###LINK_REPLACE###", "")
                                .Replace("###LINK_DISPLAY_VALUE_REPLACE###", "block")
                                .Replace("###TEXT_REPLACE5###",
                                    $"Thank you for your ongoing commitment and support.")
                                .Replace("###TEXT_REPLACE6###", "");

                            await _emailService.SendEmailAsync(user.Email, "Be Part of Research", htmlBody);
                        }
                    }

                    if (entity == null)
                    {
                        entity = new ParticipantDemographics
                        {
                            ParticipantId = request.ParticipantId,
                        };
                        updateExisting = false;
                    }
                    else
                    {
                        updateExisting = true;
                    }

                    entity.MobileNumber = request.MobileNumber;
                    entity.LandlineNumber = request.LandlineNumber;
                    entity.SexRegisteredAtBirth = request.SexRegisteredAtBirth;
                    entity.GenderIsSameAsSexRegisteredAtBirth = request.GenderIsSameAsSexRegisteredAtBirth;
                    entity.EthnicGroup = request.EthnicGroup;
                    entity.EthnicBackground = request.EthnicBackground;
                    entity.Disability = request.Disability;
                    entity.DisabilityDescription = request.DisabilityDescription;
                    entity.HealthConditionInterests = request.HealthConditionInterests?.ToList();

                    if (request.Address != null)
                    {
                        entity.Address = ParticipantAddressMapper.MapTo(request.Address);
                    }

                    if (updateExisting)
                    {
                        await _participantRepository.UpdateParticipantDemographicsAsync(entity);
                    }
                    else
                    {
                        await _participantRepository.CreateParticipantDemographicsAsync(entity);
                    }

                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(
                        ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UpdateParticipantDetailsCommand.UpdateParticipantDetailsCommandHandler), "err", ex,
                        _headerService.GetConversationId());
                    _logger.LogError(ex,
                        $"Unknown error creating participant demographics for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}