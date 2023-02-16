using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Models.StudyRegistrations;
using AutoMapper;
using Dte.Common.Exceptions;
using Domain.Entities.StudyRegistrations;
using MediatR;

namespace Application.Studies.V1.Queries.StudyRegistrations
{
    public class GetStudyRegistrationsByStatusQuery : IRequest<IEnumerable<StudyRegistrationModel>>
    {
        public StudyRegistrationStatus Status { get; }

        public GetStudyRegistrationsByStatusQuery(StudyRegistrationStatus status)
        {
            Status = status;
        }
        
        public class GetStudyRegistrationsByStatusQueryHandler : IRequestHandler<GetStudyRegistrationsByStatusQuery, IEnumerable<StudyRegistrationModel>>
        {
            private readonly IStudyRegistrationRepository _studyRegistrationRepository;
            private readonly IMapper _mapper;

            public GetStudyRegistrationsByStatusQueryHandler(IStudyRegistrationRepository studyRegistrationRepository, IMapper mapper)
            {
                _studyRegistrationRepository = studyRegistrationRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<StudyRegistrationModel>> Handle(GetStudyRegistrationsByStatusQuery request, CancellationToken cancellationToken)
            {
                var studyRegistrations = (await _studyRegistrationRepository.GetStudyRegistrationsByStatusAsync(request.Status)).ToList();

                if (!studyRegistrations.Any())
                {
                    throw new NotFoundException($"No study registrations found with status: {request.Status}");
                }

                return studyRegistrations.Select(x => _mapper.Map<StudyRegistrationModel>(x));
            }
        }
    }
}