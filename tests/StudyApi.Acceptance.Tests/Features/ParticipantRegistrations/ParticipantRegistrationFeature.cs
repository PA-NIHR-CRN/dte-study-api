// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net;
// using System.Net.Http;
// using System.Threading.Tasks;
// using Domain.Entities.ParticipantRegistrations;
// using FluentAssertions;
// using NUnit.Framework;
// using StudyApi.Acceptance.Tests.Responses;
// using StudyApi.Requests.Participants;
// using StudyApi.Requests.Studies;
// using StudyApi.Requests.StudyRegistrations;
// using TestStack.BDDfy;
//
// namespace StudyApi.Acceptance.Tests.Features.ParticipantRegistrations
// {
//     [TestFixture]
//     [Story(AsA = "site admin", IWant = "to view all participants by studyId and siteId", SoThat = "I can see my participants")]
//     public class ParticipantRegistrationFeature : AcceptanceTestBase
//     {
//         private IEnumerable<ParticipantRegistration> _responses;
//
//         [Test]
//         public void GetParticipantsByStudySite()
//         {
//             const long studyId = 12345;
//             const string siteId = "Kings College Hospital";
//             const string participantId = "participant@email.com";
//             
//             LoginAsResearcher(true);
//             
//             this.Given(_ => _.ParticipantHasAppliedToAStudySite(studyId, siteId, participantId))
//                 .And(_ => ViewAllParticipantsByStudySite(studyId, siteId))
//                 .Then(_ => CorrectNumberOfParticipantRegistrationsAreReturned(1))
//                 .And(_ => ParticipantRegistrationIsCorrect(studyId, siteId, participantId))
//                 .BDDfy();
//         }
//
//         private async Task ParticipantHasAppliedToAStudySite(long studyId, string siteId, string participantId)
//         {
//             await StudyApiClient.SendAsync<CreateStudyRegistrationRequest, ResponseBaseImpl>
//             (
//                 new CreateStudyRegistrationRequest
//                 {
//                     StudyId = studyId,
//                     Title = "Title",
//                     Researcher = new ResearcherRequest
//                     {
//                         Email = "email@email.com", Firstname = "firstname", Lastname = "lastname"
//                     }
//                 },
//                 $"{BaseAddress}api/StudyRegistrations",
//                 HttpMethod.Post,
//                 null
//             );
//             
//             await StudyApiClient.SendAsync<ApproveStudyRegistrationRequest, ResponseBaseImpl>
//             (
//                 new ApproveStudyRegistrationRequest
//                 {
//                     Title = "some title",
//                     CpmsId = 99999,
//                     IsrctnId = "888888"
//                 },
//                 $"{BaseAddress}api/StudyRegistrations/{studyId}/approve",
//                 HttpMethod.Post,
//                 null
//             );
//             
//             await StudyApiClient.SendAsync<CreateParticipantDemographicsRequest, ResponseBaseImpl>
//             (
//                 new CreateParticipantDemographicsRequest
//                 {
//                     DateOfBirth = DateTime.Today.AddYears(-20),
//                     SexRegisteredAtBirth = "male",
//                     EthnicGroup = "Any",
//                     EthnicBackground = "Any"
//                 },
//                 $"{BaseAddress}api/participants/demographics",
//                 HttpMethod.Post,
//                 null
//             );
//             
//             await StudyApiClient.SendAsync<CreateParticipantRegistrationRequest, ResponseBaseImpl>
//             (
//                 new CreateParticipantRegistrationRequest{StudyId = studyId, SiteId = siteId, ParticipantId = participantId},
//                 $"{BaseAddress}api/participants/ParticipantRegistrations",
//                 HttpMethod.Post,
//                 null
//             );
//         }
//
//         private async Task ViewAllParticipantsByStudySite(long studyId, string siteId)
//         {
//             var response = await StudyApiClient.GetParticipantRegistrationsAsync($"{BaseAddress}api/studies/{studyId}/sites/{siteId}/participants");
//             
//             Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//             
//             _responses = response.Content;
//         }
//
//         private void CorrectNumberOfParticipantRegistrationsAreReturned(int count)
//         {
//             _responses.Should().NotBeNull();
//             _responses.Should().HaveCount(count);
//         }
//         
//         private void ParticipantRegistrationIsCorrect(long studyId, string siteId, string participantId)
//         {
//             _responses.Should().NotBeNull();
//             _responses.Should().HaveCount(1);
//             _responses.ElementAt(0).StudyId.Should().Be(studyId);
//             _responses.ElementAt(0).SiteId.Should().Be(siteId);
//             _responses.ElementAt(0).ParticipantId.Should().Be(participantId);
//         }
//     }
// }