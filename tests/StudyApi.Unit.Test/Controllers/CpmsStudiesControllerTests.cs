//using System;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Application.Contracts;
//using Application.Models.Cpms;
//using Application.Models.Studies;
//using Application.Settings;
//using Application.Studies.V1.Queries.Cpms;
//using Application.Studies.V1.Queries.Studies;
//using FluentAssertions;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using NSubstitute;
//using NUnit.Framework;
//using StudyApi.Controllers.V1;
//using StudyApi.Controllers.V1.Studies;

//namespace StudyApi.Unit.Tests.Controllers
//{
//    [TestFixture]
//    public class CpmsStudiesControllerTests
//    {
//        private readonly IMediator _mediator;
        
//        private readonly CpmsStudiesController _controller;
        
//        public CpmsStudiesControllerTests()
//        {
//            _mediator = Substitute.For<IMediator>();
            
//            _controller = new CpmsStudiesController(_mediator)
//            {
//                ControllerContext = new ControllerContext
//                {
//                    HttpContext = new DefaultHttpContext{User = new ClaimsPrincipal
//                    (
//                        new ClaimsIdentity(new []{new Claim("cognito:username", Guid.NewGuid().ToString())})    
//                    )}
//                }
//            };
//        }

//        [Test]
//        public async Task GetCpmsStudy_Returns_OK()
//        {
//            const long studyId = 12345;
//            _mediator.Send(Arg.Is<GetCpmsStudyQuery>(x => x.CpmsStudyId == studyId)).Returns(new CpmsStudyModel{Id = studyId});
//            var result = await _controller.GetCpmsStudy(studyId);

//            result.Should().NotBeNull().And.Subject.Should().BeAssignableTo<OkObjectResult>();
//            result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
//            result.As<OkObjectResult>().Value.Should().NotBeNull().And.Subject.Should().BeAssignableTo<CpmsStudyModel>();
//            result.As<OkObjectResult>().Value.As<CpmsStudyModel>().Id.Should().BePositive();
//            result.As<OkObjectResult>().Value.As<CpmsStudyModel>().Id.Should().Be(studyId);
//        }
//    }
//}