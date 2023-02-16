using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Models.Cpms;
using Application.Responses.V1.Cpms;
using AutoMapper;
using Dte.Common.Exceptions;
using MediatR;

namespace Application.Studies.V1.Queries.Cpms
{
    public class GetCpmsStudyQuery : IRequest<CpmsStudyModel>
    {
        public long CpmsStudyId { get; }

        private const string NotFoundErrorMessage = "Study does not exist";

        public GetCpmsStudyQuery(long cpmsStudyId)
        {
            CpmsStudyId = cpmsStudyId;
        }
        
        public class GetCpmsStudyQueryHandler : IRequestHandler<GetCpmsStudyQuery, CpmsStudyModel>
        {
            private readonly ICpmsHttpClient _cpmsHttpClient;
            private readonly IMapper _mapper;

            public GetCpmsStudyQueryHandler(ICpmsHttpClient cpmsHttpClient, IMapper mapper)
            {
                _cpmsHttpClient = cpmsHttpClient;
                _mapper = mapper;
            }

            public async Task<CpmsStudyModel> Handle(GetCpmsStudyQuery request, CancellationToken cancellationToken)
            {
                var cpmsApiResponseRoot = await _cpmsHttpClient.GetStudyAsync(request.CpmsStudyId);

                if (cpmsApiResponseRoot?.Result?.Study == null && cpmsApiResponseRoot?.Result?.Result?.Errors != null)
                {
                    throw cpmsApiResponseRoot.Result.Result.Errors.Contains(NotFoundErrorMessage)
                        ? new NotFoundException("CPMS API Study", request.CpmsStudyId)
                        : new Exception($"Error calling CPMS API: StudyId: {request.CpmsStudyId} - {string.Join(", ", cpmsApiResponseRoot.Result.Result.Errors)}");
                }

                return _mapper.Map<CpmsApiResponse, CpmsStudyModel>(cpmsApiResponseRoot?.Result?.Study);
            }
        }
    }
}