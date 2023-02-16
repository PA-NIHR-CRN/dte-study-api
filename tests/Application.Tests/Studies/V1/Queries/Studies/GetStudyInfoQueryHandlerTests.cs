// using System.Threading;
// using Application.Contracts;
// using Application.Studies.V1.Queries.Studies;
// using AutoMapper;
// using Domain.Entities.Studies;
// using Dte.Common.Exceptions;
// using NSubstitute;
// using NUnit.Framework;
//
// namespace Application.Tests.Studies.V1.Queries.Studies
// {
//     [TestFixture]
//     public class GetStudyInfoQueryHandlerTests
//     {
//         private readonly IStudyRepository _studyRepository;
//         private readonly IMapper _mapper;
//         
//         private readonly GetStudyInfoQuery.GetStudyInfoQueryHandler _handler;
//
//         public GetStudyInfoQueryHandlerTests()
//         {
//             _studyRepository = Substitute.For<IStudyRepository>();
//             _mapper = Substitute.For<IMapper>();
//
//             _handler = new GetStudyInfoQuery.GetStudyInfoQueryHandler(_studyRepository, _mapper);
//         }
//
//         [Test]
//         public void Handle_Study_Not_Found_Returns_NotFoundException()
//         {
//             const long studyId = 12345L;
//             var request = new GetStudyInfoQuery(studyId);
//
//             _studyRepository.GetStudyAsync(studyId).ReturnsForAnyArgs((Study)null);
//
//             Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
//         }
//         
//         [Test]
//         public void Handle_StudyInfoResponse_Returned()
//         {
//             const long studyId = 12345L;
//             var request = new GetStudyInfoQuery(studyId);
//
//             _studyRepository.GetStudyAsync(studyId).ReturnsForAnyArgs((Study)null);
//
//             Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
//         }
//     }
// }