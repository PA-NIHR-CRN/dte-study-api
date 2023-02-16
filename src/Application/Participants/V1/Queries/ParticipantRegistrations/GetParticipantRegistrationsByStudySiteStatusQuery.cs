using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Models.ParticipantRegistrations;
using AutoMapper;
using Dte.Common.Exceptions;
using Domain.Entities.ParticipantRegistrations;
using MediatR;

namespace Application.Participants.V1.Queries.ParticipantRegistrations
{
    public class GetParticipantRegistrationsByStudySiteStatusQuery : IRequest<IEnumerable<ParticipantRegistrationModel>>
    {
        public long StudyId { get; }
        public string SiteId { get; }
        public ParticipantRegistrationStatus Status { get; }

        public GetParticipantRegistrationsByStudySiteStatusQuery(long studyId, string siteId, ParticipantRegistrationStatus status)
        {
            StudyId = studyId;
            SiteId = siteId;
            Status = status;
        }
        
        public class GetParticipantRegistrationsByStudySiteStatusQueryHandler : IRequestHandler<GetParticipantRegistrationsByStudySiteStatusQuery, IEnumerable<ParticipantRegistrationModel>>
        {
            private readonly IParticipantRegistrationRepository _participantRegistrationRepository;
            private readonly IMapper _mapper;

            public GetParticipantRegistrationsByStudySiteStatusQueryHandler(IParticipantRegistrationRepository participantRegistrationRepository, IMapper mapper)
            {
                _participantRegistrationRepository = participantRegistrationRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ParticipantRegistrationModel>> Handle(GetParticipantRegistrationsByStudySiteStatusQuery request, CancellationToken cancellationToken)
            {
                var participantRegistrations = (await _participantRegistrationRepository.GetParticipantsByStudySiteStatusAsync(request.StudyId, request.SiteId, request.Status)).ToList();
                
                if (!participantRegistrations.Any())
                {
                    throw new NotFoundException($"No participant registrations found for studyId: {request.StudyId} and siteId: {request.SiteId} and participant status: {request.Status}");
                }

                return participantRegistrations.Select(x => _mapper.Map<ParticipantRegistrationModel>(x));
            }
        }
    }
}