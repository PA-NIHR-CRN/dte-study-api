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
    public class GetParticipantRegistrationsStatusByStudyQuery : IRequest<IEnumerable<ParticipantRegistrationModel>>
    {
        public long StudyId { get; set; }
        public ParticipantRegistrationStatus Status { get; }

        public GetParticipantRegistrationsStatusByStudyQuery(long studyId, ParticipantRegistrationStatus status)
        {
            StudyId = studyId;
            Status = status;
        }
        
        public class GetParticipantRegistrationsStatusByStudyQueryHandler : IRequestHandler<GetParticipantRegistrationsStatusByStudyQuery, IEnumerable<ParticipantRegistrationModel>>
        {
            private readonly IParticipantRegistrationRepository _participantRegistrationRepository;
            private readonly IMapper _mapper;

            public GetParticipantRegistrationsStatusByStudyQueryHandler(IParticipantRegistrationRepository participantRegistrationRepository, IMapper mapper)
            {
                _participantRegistrationRepository = participantRegistrationRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ParticipantRegistrationModel>> Handle(GetParticipantRegistrationsStatusByStudyQuery request, CancellationToken cancellationToken)
            {
                var participantRegistrations = (await _participantRegistrationRepository.GetParticipantRegistrationsStatusByStudy(request.StudyId, request.Status)).ToList();

                if (!participantRegistrations.Any())
                {
                    throw new NotFoundException($"No participant registrations found with status: {request.Status}");
                }

                return participantRegistrations.Select(x => _mapper.Map<ParticipantRegistrationModel>(x));
            }
        }
    }
}