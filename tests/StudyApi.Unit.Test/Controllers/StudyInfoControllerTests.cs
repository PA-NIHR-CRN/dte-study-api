using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Models.Studies;
using Application.Responses;
using Application.Responses.V1.Studies;
using Application.Studies.V1.Queries.Studies;
using Dte.Common.Responses;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using StudyApi.Controllers.V1.Studies;

namespace StudyApi.Unit.Tests.Controllers
{
    [TestFixture]
    public class StudyInfoControllerTests
    {
        private readonly IMediator _mediator;
        
        private readonly StudyInfoController _controller;
        
        public StudyInfoControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            
            _controller = new StudyInfoController(_mediator)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext{User = new ClaimsPrincipal
                    (
                        new ClaimsIdentity(new []{new Claim("cognito:username", Guid.NewGuid().ToString())})    
                    )}
                }
            };
        }

       /* [Test]
        public async Task GetStudyInfo_Returns_Ok()
        {
            const long studyId = 12345;
            var studyModels = new List<StudyModel> { new StudyModel{StudyId = studyId} };
            var studyInfoResponse = new StudyInfoResponse{Title = "title"};
            _mediator.Send(Arg.Any<GetStudyInfoQuery>()).Returns(Response<StudyInfoResponse>.CreateSuccessfulContentResponse(studyInfoResponse));
            
            var result = await _controller.GetStudyInfo(studyId);

            result.Should().NotBeNull().And.Subject.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.Subject.Should().BeAssignableTo<Response<StudyInfoResponse>>();
            result.As<OkObjectResult>().Value.As<Response<StudyInfoResponse>>().Content.Should().NotBeNull().And.Subject.Should().BeEquivalentTo(studyInfoResponse);
            
            await _mediator.Received().Send(Arg.Any<GetStudyInfoQuery>());
        }*/
    }
}