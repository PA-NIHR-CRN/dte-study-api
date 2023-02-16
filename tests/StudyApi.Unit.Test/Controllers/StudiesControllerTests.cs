// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Security.Claims;
// using System.Threading.Tasks;
// using Application.Contracts;
// using Application.Extensions;
// using Application.Models.Studies;
// using Application.Responses;
// using Application.Responses.V1.Studies;
// using Application.Studies.V1.Queries.Studies;
// using Dte.Common.Responses;
// using FluentAssertions;
// using MediatR;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using NSubstitute;
// using NUnit.Framework;
// using StudyApi.Controllers.V1.Studies;
//
// namespace StudyApi.Unit.Tests.Controllers
// {
//     [TestFixture]
//     public class StudiesControllerTests
//     {
//         private readonly IMediator _mediator;
//
//         private readonly StudiesController _controller;
//         
//         public StudiesControllerTests()
//         {
//             _mediator = Substitute.For<IMediator>();
//             var researcherStudyValidationService = Substitute.For<IResearcherStudyValidationService>();
//             
//             _controller = new StudiesController(_mediator, researcherStudyValidationService)
//             {
//                 ControllerContext = new ControllerContext
//                 {
//                     HttpContext = new DefaultHttpContext{User = new ClaimsPrincipal
//                     (
//                         new ClaimsIdentity(new []{new Claim("cognito:username", Guid.NewGuid().ToString())})    
//                     )}
//                 }
//             };
//         }
//         
//         [Test]
//         public async Task GetAllMyStudies_Returns_OK()
//         {
//             const long studyId = 12345;
//             var studyModels = new List<StudyModel> { new StudyModel{StudyId = studyId} };
//             
//             _mediator.Send(Arg.Any<GetAllUsersStudiesQuery>()).Returns(Response<PaginationListResponse<StudyModel>>.CreateSuccessfulContentResponse(new PaginationListResponse<StudyModel>
//             {
//                 PaginationToken = null, Items = studyModels
//             }));
//             
//             var result = await _controller.GetAllMyStudies();
//
//             result.Should().NotBeNull().And.Subject.Should().BeAssignableTo<OkObjectResult>();
//             result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
//             result.As<OkObjectResult>().Value.Should().NotBeNull().And.Subject.Should().BeAssignableTo<Response<PaginationListResponse<StudyModel>>>();
//             result.As<OkObjectResult>().Value.As<Response<PaginationListResponse<StudyModel>>>().Content.Should().NotBeNull();
//             result.As<OkObjectResult>().Value.As<Response<PaginationListResponse<StudyModel>>>().Content.Items.Should().BeEquivalentTo(studyModels);
//             
//             await _mediator.Received().Send(Arg.Any<GetAllUsersStudiesQuery>());
//         }
//         
//         [Test]
//         public async Task GetStudy_Returns_OK()
//         {
//             const long studyId = 12345;
//             var studyModel = new StudyModel{StudyId = studyId};
//             var getStudyResponse = new StudyRoleResponse{ Role = "None", Item = studyModel};
//             
//             _mediator.Send(Arg.Is<GetStudyQuery>(x => x.StudyId == studyId && x.ResearcherId == _controller.User.GetUserIdCognito()))
//                 .Returns(Response<StudyRoleResponse>.CreateSuccessfulContentResponse(getStudyResponse));
//             
//             var result = await _controller.GetStudy(studyId);
//
//             result.Should().NotBeNull().And.Subject.Should().BeAssignableTo<OkObjectResult>();
//             result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
//             result.As<OkObjectResult>().Value.Should().NotBeNull().And.Subject.Should().BeAssignableTo<Response<StudyRoleResponse>>();
//             result.As<OkObjectResult>().Value.As<Response<StudyRoleResponse>>().Content.Item.StudyId.Should().Be(studyId);
//             
//             await _mediator.Received().Send(Arg.Is<GetStudyQuery>(x => x.StudyId == studyId && x.ResearcherId == _controller.User.GetUserIdCognito()));
//         }
//         
//         [Test]
//         public async Task GetStudySites_Returns_OK()
//         {
//             const long studyId = 12345;
//             var sites = new List<StudySiteModel>{new StudySiteModel()};
//             var expectedPaginatedResponse = new PaginationListResponse<StudySiteModel>{Items = sites};
//             
//             _mediator.Send(Arg.Is<GetStudySitesPagedQuery>(x => x.StudyId == studyId))
//                 .Returns(Response<PaginationListResponse<StudySiteModel>>.CreateSuccessfulContentResponse(expectedPaginatedResponse));
//             
//             var result = await _controller.GetStudySites(studyId);
//
//             result.Should().NotBeNull().And.Subject.Should().BeAssignableTo<OkObjectResult>();
//             result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
//             result.As<OkObjectResult>().Value.Should().NotBeNull().And.Subject.Should().BeAssignableTo<Response<PaginationListResponse<StudySiteModel>>>();
//             result.As<OkObjectResult>().Value.As<Response<PaginationListResponse<StudySiteModel>>>().Content.Items.Should().NotBeNull().And.Subject.Should().BeEquivalentTo(sites);
//             
//             await _mediator.Received().Send(Arg.Is<GetStudySitesPagedQuery>(x => x.StudyId == studyId));
//         }
//         
//         [Test]
//         public async Task GetStudySitesGroupedByStatus_Returns_OK()
//         {
//             const long studyId = 12345;
//             var sites = new List<StudySiteModel>{new StudySiteModel{Status = "Any"}};
//             var expectedSites = sites.GroupBy(x => x.Status).ToDictionary(x => x.Key);
//             
//             _mediator.Send(Arg.Is<GetStudySitesQuery>(x => x.StudyId == studyId))
//                 .Returns(Response<Dictionary<string, IGrouping<string, StudySiteModel>>>.CreateSuccessfulContentResponse(expectedSites));
//             
//             var result = await _controller.GetStudySitesGroupedByStatus(studyId);
//
//             result.Should().NotBeNull().And.Subject.Should().BeAssignableTo<OkObjectResult>();
//             result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
//             result.As<OkObjectResult>().Value.Should().NotBeNull().And.Subject.Should().BeAssignableTo<Response<Dictionary<string, IGrouping<string, StudySiteModel>>>>();
//             
//             await _mediator.Received().Send(Arg.Is<GetStudySitesQuery>(x => x.StudyId == studyId));
//         }
//     }
// }