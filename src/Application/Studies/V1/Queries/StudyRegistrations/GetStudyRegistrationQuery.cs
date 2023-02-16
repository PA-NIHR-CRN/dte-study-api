using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Models.StudyRegistrations;
using AutoMapper;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;

namespace Application.Studies.V1.Queries.StudyRegistrations
{
    public class GetStudyRegistrationQuery : IRequest<Response<StudyRegistrationModel>>
    {
        public long StudyId { get; }

        public GetStudyRegistrationQuery(long studyId)
        {
            StudyId = studyId;
        }
        
        public class GetStudyRegistrationQueryHandler : IRequestHandler<GetStudyRegistrationQuery, Response<StudyRegistrationModel>>
        {
            private readonly IStudyRegistrationRepository _studyRegistrationRepository;
            private readonly IMapper _mapper;
            private readonly IHeaderService _headerService;

            public GetStudyRegistrationQueryHandler(IStudyRegistrationRepository studyRegistrationRepository, IMapper mapper, IHeaderService headerService)
            {
                _studyRegistrationRepository = studyRegistrationRepository;
                _mapper = mapper;
                _headerService = headerService;
            }

            public async Task<Response<StudyRegistrationModel>> Handle(GetStudyRegistrationQuery request, CancellationToken cancellationToken)
            {
                var studyRegistration = await _studyRegistrationRepository.GetStudyRegistrationAsync(request.StudyId);

                if (studyRegistration == null)
                {
                    return Response<StudyRegistrationModel>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetStudyRegistrationQuery), ErrorCode.StudyRegistrationNotFoundError, $"Could not find study registration: {request.StudyId}", _headerService.GetConversationId());
                }

                var model = _mapper.Map<StudyRegistrationModel>(studyRegistration);
                
                return Response<StudyRegistrationModel>.CreateSuccessfulContentResponse(model, _headerService.GetConversationId());
            }
        }
    }
}