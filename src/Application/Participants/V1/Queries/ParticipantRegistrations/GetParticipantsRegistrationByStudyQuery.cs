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
    public class GetParticipantsRegistrationByStudyQuery : IRequest<IEnumerable<ParticipantRegistrationModel>>
    {
        public long StudyId { get; }
        public string ParticipantId { get; }

        public GetParticipantsRegistrationByStudyQuery(long studyId, string participantId)
        {
            StudyId = studyId;
            ParticipantId = participantId;
        }
        
        public class GetParticipantsByStudyQueryHandler : IRequestHandler<GetParticipantsRegistrationByStudyQuery, IEnumerable<ParticipantRegistrationModel>>
        {
            private readonly IParticipantRegistrationRepository _participantRegistrationRepository;
            private readonly IMapper _mapper;

            public GetParticipantsByStudyQueryHandler(IParticipantRegistrationRepository participantRegistrationRepository, IMapper mapper)
            {
                _participantRegistrationRepository = participantRegistrationRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ParticipantRegistrationModel>> Handle(GetParticipantsRegistrationByStudyQuery request, CancellationToken cancellationToken)
            {
                var participantRegistrations = (await _participantRegistrationRepository.GetParticipantsByStudyAsync(request.StudyId, request.ParticipantId)).ToList();
                
                if (!participantRegistrations.Any())
                {
                    throw new NotFoundException($"No participant registrations found for studyId: {request.StudyId} and participantId: {request.ParticipantId}");
                }

                return participantRegistrations.Select(x => _mapper.Map<ParticipantRegistrationModel>(x));
            }
        }
    }
}