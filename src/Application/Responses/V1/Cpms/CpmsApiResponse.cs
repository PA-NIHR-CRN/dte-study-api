using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Responses.V1.Cpms
{
    public class CpmsApiResponseRoot
    {
        public string Version { get; set; }
        public long StatusCode { get; set; }
        public StudyResult Result { get; set; }
    }

    public class StudyResult
    {
        public CpmsApiResponse Study { get; set; }
        public ResultResult Result { get; set; }
    }

    public class ResultResult
    {
        public string Result { get; set; }
        public List<string> Errors { get; set; }
        public object DetailedErrors { get; set; }
        public object Entity { get; set; }
    }
    
    public class CpmsApiResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        public string ShortName { get; set; }
        public string Title { get; set; }
        public long? IrasId { get; set; }
        public bool? RecordDeleted { get; set; }
        public string RecReference { get; set; }
        public object ClinicalTrialGovReference { get; set; }
        public object IsrctnNumber { get; set; }
        public object EudraCtNumber { get; set; }
        public string ResearchSummary { get; set; }
        public long? UkRecruitmentSampleSize { get; set; }
        public object NetworkRecruitmentSampleSize { get; set; }
        public long? GlobalRecruitmentSampleSize { get; set; }
        public DateTimeOffset? PlannedRecruitmentStartDate { get; set; }
        public DateTimeOffset? PlannedRecruitmentEndDate { get; set; }
        public string InclusionCriteria { get; set; }
        public string ExclusionCriteria { get; set; }
        public object UpperAgeLimit { get; set; }
        public string UpperAgeLimitMetric { get; set; }
        public long? LowerAgeLimit { get; set; }
        public string LowerAgeLimitMetric { get; set; }
        public object StudyWebsite { get; set; }
        public object Email { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string TargetGender { get; set; }
        public StudyDesignResponse StudyDesign { get; set; }
        public CommercialParticipationDegreeResponse Status { get; set; }
        public List<StudyDiseaseResponse> StudyDiseases { get; set; }
        public List<StudyHrcResponse> StudyHrcs { get; set; }
        public List<StudyOrganisationResponse> StudyOrganisations { get; set; }
        public List<StudySpecialtyResponse> StudySpecialties { get; set; }
        public List<CommercialParticipationDegreeResponse> StudyPhases { get; set; }
        public List<object> StudyInterventions { get; set; }
        public List<object> StudyOutcomes { get; set; }
        public List<StudyPersonResponse> StudyPerson { get; set; }
        public List<StudyConsumerParticipationDegreeElementResponse> StudySiteCountries { get; set; }
        public List<object> StudyPublications { get; set; }
        public CommercialParticipationDegreeResponse StudyRouteId { get; set; }
        public DateTimeOffset? QualificationDate { get; set; }
        public bool? IsOpenToNewSites { get; set; }
        public bool? IsNonCrnnihrInfrastructureSupport { get; set; }
        public bool? IsStudyRecruitmentUploadRequired { get; set; }
        public object DoesStudyHaveScreeningElement { get; set; }
        public CommercialParticipationDegreeResponse ResearchActivityUploadMethod { get; set; }
        public DateTimeOffset? LpmsResearchActivityUploadStartDate { get; set; }
        public ManualUploadMethodReasonResponse ManualUploadMethodReason { get; set; }
        public long? WalesRecruitmentSampleSize { get; set; }
        public long? EnglandRecruitmentSampleSize { get; set; }
        public long? ScotlandRecruitmentSampleSize { get; set; }
        public long? NiRecruitmentSampleSize { get; set; }
        public DateTimeOffset? ActualOpeningDate { get; set; }
        public DateTimeOffset? ActualClosureDate { get; set; }
        public bool? ConfirmedAsClosed { get; set; }
        public List<StudyConsumerParticipationDegreeElementResponse> StudySettings { get; set; }
        public CommercialParticipationDegreeResponse EligibilityStatusId { get; set; }
        public CommercialParticipationDegreeResponse LeadAdministrationId { get; set; }
        public long? ParticipantsRecruitedToDate { get; set; }
        public List<StudyConsumerParticipationDegreeElementResponse> StudyConsumerParticipationDegree { get; set; }
        public string ConsumerParticipationDetail { get; set; }
        public List<object> StudyConsumerParticipationDetail { get; set; }
        public CommercialParticipationDegreeResponse GeographicalScopeId { get; set; }
        public CommercialParticipationDegreeResponse LeadRnDCountryId { get; set; }
        public CommercialParticipationDegreeResponse CommercialParticipationDegree { get; set; }
    }

    public class CommercialParticipationDegreeResponse
    {
        public string Name { get; set; }
        public string RtsIdentifier { get; set; }
    }

    public class ManualUploadMethodReasonResponse
    {
        public object UploadMethodReason { get; set; }
        public object RtsIdentifier { get; set; }
        public object UploadMethodReasonNote { get; set; }
    }

    public class StudyConsumerParticipationDegreeElementResponse
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string RtsIdentifier { get; set; }
    }

    public class StudyDesignResponse
    {
        public CommercialParticipationDegreeResponse StudyDesignType { get; set; }
        public string InterventionDescription { get; set; }
        public object ObervationalDetail { get; set; }
        public List<CommercialParticipationDegreeResponse> Interventional { get; set; }
        public List<CommercialParticipationDegreeResponse> InterventionalDetail { get; set; }
    }

    public class StudyDiseaseResponse
    {
        public CommercialParticipationDegreeResponse Disease { get; set; }
    }

    public class StudyHrcResponse
    {
        public CommercialParticipationDegreeResponse Category { get; set; }
    }

    public class StudyOrganisationResponse
    {
        public string Name { get; set; }
        public string RtsIdentifier { get; set; }
        public CommercialParticipationDegreeResponse OrgRoleName { get; set; }
        public List<StudyFundingResponse> StudyFundings { get; set; }
        public List<StudySiteResponse> StudySites { get; set; }
    }

    public class StudyFundingResponse
    {
        public string Name { get; set; }
        public string GrantCode { get; set; }
        public string RtsIdentifier { get; set; }
    }

    public class StudySiteResponse
    {
        public string RtsIdentifier { get; set; }
        public string Name { get; set; }
        public object NetworkRecruitmentTarget { get; set; }
        public DateTimeOffset? PlannedRecruitmentStartDate { get; set; }
        public DateTimeOffset? PlannedRecruitmentEndDate { get; set; }
        public DateTimeOffset? ActualRecruitmentStartDate { get; set; }
        public object ActualRecruitmentEndDate { get; set; }
        public ParentOrganisationAsSiteResponse ParentOrganisationAsSite { get; set; }
    }

    public class ParentOrganisationAsSiteResponse
    {
        public object ParentOrganisationAsSiteReason { get; set; }
        public object ParentOrganisationAsSiteReasonNote { get; set; }
        public object RtsIdentifier { get; set; }
    }

    public class StudyPersonResponse
    {
        public string Role { get; set; }
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }

    public class StudySpecialtyResponse
    {
        public bool? IsManagingSpecialty { get; set; }
        public string Specialty { get; set; }
        public string RtsIdentifier { get; set; }
        public List<StudySpecialtySubspecialtyResponse> StudySpecialtySubspecialties { get; set; }
    }

    public class StudySpecialtySubspecialtyResponse
    {
        public bool? IsPrimary { get; set; }
        public string SubSpecialty { get; set; }
        public string RtsIdentifier { get; set; }
    }
}