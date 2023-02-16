using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Models.ParticipantRegistrations;
using AutoMapper;
using Dte.Common.Exceptions;
using MediatR;

namespace Application.Participants.V1.Queries.ParticipantRegistrations
{
    public class GetParticipantRegistrationsByStudySiteQuery : IRequest<IEnumerable<ParticipantRegistrationModel>>
    {
        public long StudyId { get; }
        public string SiteId { get; }

        public GetParticipantRegistrationsByStudySiteQuery(long studyId, string siteId)
        {
            StudyId = studyId;
            SiteId = siteId; 
        }

        public class GetParticipantsByStudySiteQueryHandler : IRequestHandler<GetParticipantRegistrationsByStudySiteQuery, IEnumerable<ParticipantRegistrationModel>>
        {
            private readonly IParticipantRegistrationRepository _participantRegistrationRepository;
            private readonly IMapper _mapper;

            public GetParticipantsByStudySiteQueryHandler(IParticipantRegistrationRepository participantRegistrationRepository, IMapper mapper)
            {
                _participantRegistrationRepository = participantRegistrationRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ParticipantRegistrationModel>> Handle(GetParticipantRegistrationsByStudySiteQuery request, CancellationToken cancellationToken)
            {
                var participantRegistrations = (await _participantRegistrationRepository.GetParticipantsByStudySiteAsync(request.StudyId, request.SiteId)).ToList();
                
                if (!participantRegistrations.Any())
                {
                    throw new NotFoundException($"No participant registrations found for studyId: {request.StudyId} and siteId: {request.SiteId}");
                }

                return participantRegistrations.Select(x => _mapper.Map<ParticipantRegistrationModel>(x));
            }
        }
    }
}