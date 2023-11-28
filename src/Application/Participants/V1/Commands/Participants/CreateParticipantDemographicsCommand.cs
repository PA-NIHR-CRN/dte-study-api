using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Mappings.Participants;
using Application.Models.Participants;
using Domain.Entities.Participants;
using Dte.Common;
using Dte.Common.Contracts;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Models;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Participants.V1.Commands.Participants
{
    public class CreateParticipantDemographicsCommand : IRequest<Response<object>>
    {
        private const string DefaultLocale = "en-GB";
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
            private readonly IHeaderService _headerService;
            private readonly ILogger<CreateParticipantDemographicsCommandHandler> _logger;
            private readonly IEmailService _emailService;
            private readonly IContentfulService _contentfulService;
            private readonly ContentfulSettings _contentfulSettings;

            public CreateParticipantDemographicsCommandHandler(IParticipantRepository participantRepository,
                IHeaderService headerService, ILogger<CreateParticipantDemographicsCommandHandler> logger,
                IEmailService emailService, IContentfulService contentfulService, ContentfulSettings contentfulSettings)
            {
                _participantRepository = participantRepository;
                _headerService = headerService;
                _logger = logger;
                _emailService = emailService;
                _contentfulService = contentfulService;
                _contentfulSettings = contentfulSettings;
            }

            public async Task<Response<object>> Handle(CreateParticipantDemographicsCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var updateExisting = false;

                    var entity = await _participantRepository.GetParticipantDemographicsAsync(request.ParticipantId);

                    _logger.LogInformation("Participant: {SerializeObject}", JsonConvert.SerializeObject(entity));

                    if (!entity.HasDemographics)
                    {
                        var user = await _participantRepository.GetParticipantDetailsAsync(request.ParticipantId);
                        if (user.NhsId is null)
                        {
                            _logger.LogInformation(
                                "Sending email with name {EmailTemplatesNewAccount} to {UserEmail} for participant {UserParticipantId}",
                                _contentfulSettings.EmailTemplates.NewAccount, user.Email, user.ParticipantId);

                            var contentfulEmailRequest = new EmailContentRequest
                            {
                                EmailName = _contentfulSettings.EmailTemplates.NewAccount,
                                SelectedLocale = new CultureInfo(user.SelectedLocale ?? SelectedLocale.Default),
                            };
                            
                            _logger.LogInformation("ContentfulEmailRequest: {SerializeObject}",
                                JsonConvert.SerializeObject(contentfulEmailRequest));

                            var contentfulEmail = await _contentfulService.GetEmailContentAsync(contentfulEmailRequest);
                            
                            _logger.LogInformation("ContentfulEmail: {SerializeObject}",
                                JsonConvert.SerializeObject(contentfulEmail));

                            await _emailService.SendEmailAsync(user.Email, contentfulEmail.EmailSubject,
                                contentfulEmail.EmailBody);
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
                        _logger.LogInformation("Updating participant demographics for {ParticipantId}",
                            request.ParticipantId);
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
                        nameof(CreateParticipantDemographicsCommandHandler), "err", ex,
                        _headerService.GetConversationId());
                    _logger.LogError(ex,
                        $"Unknown error creating participant demographics for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}
