using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Models.Researchers;
using Dte.Common.Contracts;
using Domain.Entities.StudyRegistrations;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;

namespace Application.Studies.V1.Commands.StudyRegistrations
{
    public class CreateStudyRegistrationCommand : IRequest<Response<object>>
    {
        public long StudyId { get; set; }
        public string Title { get; set; }
        public ResearcherModel Researcher { get; set; }

        public CreateStudyRegistrationCommand(long studyId, string title, ResearcherModel researcher)
        {
            StudyId = studyId;
            Title = title;
            Researcher = researcher;
        }
        
        public class CreateStudyRegistrationCommandHandler : IRequestHandler<CreateStudyRegistrationCommand, Response<object>>
        {
            private readonly IStudyRegistrationRepository _studyRegistrationRepository;
            private readonly IClock _clock;
            private readonly IHeaderService _headerService;

            public CreateStudyRegistrationCommandHandler(IStudyRegistrationRepository studyRegistrationRepository, IClock clock, IHeaderService headerService)
            {
                _studyRegistrationRepository = studyRegistrationRepository;
                _clock = clock;
                _headerService = headerService;
            }
            
            public async Task<Response<object>> Handle(CreateStudyRegistrationCommand request, CancellationToken cancellationToken)
            {
                var studyRegistration = await _studyRegistrationRepository.GetStudyRegistrationAsync(request.StudyId);

                // TODO - rules Engine or FluentValidation
                if (studyRegistration != null && 
                    (studyRegistration.StudyRegistrationStatus == StudyRegistrationStatus.Delayed ||
                     studyRegistration.StudyRegistrationStatus == StudyRegistrationStatus.None || 
                     studyRegistration.StudyRegistrationStatus == StudyRegistrationStatus.WaitingForApproval || 
                     studyRegistration.StudyRegistrationStatus == StudyRegistrationStatus.Approved) )
                {
                    var errorMessage = $"Study registration already exists for studyId: {request.StudyId} - Status: {studyRegistration.StudyRegistrationStatus}";
                    return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(CreateStudyRegistrationCommand), ErrorCode.StudyRegistrationAlreadyExistsError, errorMessage, _headerService.GetConversationId());
                }
                
                var entity = new StudyRegistration
                {
                    StudyId = request.StudyId,
                    Title = request.Title,
                    ResearcherId = request.Researcher.Id,
                    ResearcherFirstname = request.Researcher.Firstname,
                    ResearcherLastname = request.Researcher.Lastname,
                    ResearcherEmail = request.Researcher.Email,
                    SubmittedAtUtc = _clock.Now(),
                    StudyRegistrationStatus = StudyRegistrationStatus.None
                };
                
                await _studyRegistrationRepository.CreateStudyRegistrationAsync(entity);
                
                return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
            }
        }
    }
}