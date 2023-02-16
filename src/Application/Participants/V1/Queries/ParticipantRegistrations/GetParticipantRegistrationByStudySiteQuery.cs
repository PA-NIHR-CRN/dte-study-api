using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Models.ParticipantRegistrations;
using AutoMapper;
using Dte.Common.Exceptions;
using MediatR;

namespace Application.Participants.V1.Queries.ParticipantRegistrations
{
    public class GetParticipantRegistrationByStudySiteQuery : IRequest<ParticipantRegistrationModel>
    {
        public long StudyId { get; }
        public string SiteId { get; }
        public string ParticipantId { get; }

        public GetParticipantRegistrationByStudySiteQuery(long studyId, string siteId, string participantId)
        {
            StudyId = studyId;
            SiteId = siteId;
            ParticipantId = participantId;
        }
        
        public class GetParticipantByStudySiteQueryHandler : IRequestHandler<GetParticipantRegistrationByStudySiteQuery, ParticipantRegistrationModel>
        {
            private readonly IParticipantRegistrationRepository _participantRegistrationRepository;
            private readonly IMapper _mapper;

            public GetParticipantByStudySiteQueryHandler(IParticipantRegistrationRepository participantRegistrationRepository, IMapper mapper)
            {
                _participantRegistrationRepository = participantRegistrationRepository;
                _mapper = mapper;
            }

            public async Task<ParticipantRegistrationModel> Handle(GetParticipantRegistrationByStudySiteQuery request, CancellationToken cancellationToken)
            {
                var participantRegistration = (await _participantRegistrationRepository.GetParticipantByStudySiteAsync(request.StudyId, request.SiteId, request.ParticipantId));
                
                if (participantRegistration == null)
                {
                    throw new NotFoundException($"No participant registration found for studyId: {request.StudyId} and siteId: {request.SiteId} and participantId: {request.ParticipantId}");
                }

                return _mapper.Map<ParticipantRegistrationModel>(participantRegistration);
            }
        }
    }
}