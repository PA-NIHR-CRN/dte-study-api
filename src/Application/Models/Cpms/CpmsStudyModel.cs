using System;
using System.Collections.Generic;
using System.Linq;
using Application.Mappings;
using Application.Mappings.AutoMapper;
using Application.Responses.V1.Cpms;
using AutoMapper;

namespace Application.Models.Cpms
{
    public class CpmsStudyModel : IMapFrom<CpmsApiResponse>
    {
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
        public long GlobalRecruitmentSampleSize { get; set; }
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
        public StudyDesign StudyDesign { get; set; }
        public CommercialParticipationDegree Status { get; set; }
        public List<StudyDisease> StudyDiseases { get; set; }
        public List<StudyHrc> StudyHrcs { get; set; }
        public List<StudyOrganisation> StudyOrganisations { get; set; }
        public List<StudySpecialty> StudySpecialties { get; set; }
        public List<CommercialParticipationDegree> StudyPhases { get; set; }
        public List<object> StudyInterventions { get; set; }
        public List<object> StudyOutcomes { get; set; }
        public List<StudyPerson> StudyPerson { get; set; }
        public List<StudyConsumerParticipationDegreeElement> StudySiteCountries { get; set; }
        public List<object> StudyPublications { get; set; }
        public CommercialParticipationDegree StudyRouteId { get; set; }
        public DateTimeOffset? QualificationDate { get; set; }
        public bool? IsOpenToNewSites { get; set; }
        public bool? IsNonCrnnihrInfrastructureSupport { get; set; }
        public bool? IsStudyRecruitmentUploadRequired { get; set; }
        public object DoesStudyHaveScreeningElement { get; set; }
        public CommercialParticipationDegree ResearchActivityUploadMethod { get; set; }
        public DateTimeOffset? LpmsResearchActivityUploadStartDate { get; set; }
        public ManualUploadMethodReason ManualUploadMethodReason { get; set; }
        public long? WalesRecruitmentSampleSize { get; set; }
        public long? EnglandRecruitmentSampleSize { get; set; }
        public long? ScotlandRecruitmentSampleSize { get; set; }
        public long? NiRecruitmentSampleSize { get; set; }
        public DateTimeOffset? ActualOpeningDate { get; set; }
        public DateTimeOffset? ActualClosureDate { get; set; }
        public bool? ConfirmedAsClosed { get; set; }
        public List<StudyConsumerParticipationDegreeElement> StudySettings { get; set; }
        public CommercialParticipationDegree EligibilityStatusId { get; set; }
        public CommercialParticipationDegree LeadAdministrationId { get; set; }
        public long? ParticipantsRecruitedToDate { get; set; }
        public List<StudyConsumerParticipationDegreeElement> StudyConsumerParticipationDegree { get; set; }
        public string ConsumerParticipationDetail { get; set; }
        public List<object> StudyConsumerParticipationDetail { get; set; }
        public CommercialParticipationDegree GeographicalScopeId { get; set; }
        public CommercialParticipationDegree LeadRnDCountryId { get; set; }
        public CommercialParticipationDegree CommercialParticipationDegree { get; set; }
        
        // Dynamic Property to map all sites
        public List<StudySite> Sites { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyDesignResponse, StudyDesign>();
            profile.CreateMap<CpmsApiResponse, CpmsStudyModel>()
                .ForMember(x => x.StudyDesign, opts => opts.MapFrom(x => x.StudyDesign))
                .ForMember(x => x.Sites, opts => opts.MapFrom(x => x.StudyOrganisations.SelectMany(y => y.StudySites)));
        }
    }
    
    public class CommercialParticipationDegree : IMapFrom<CommercialParticipationDegreeResponse>
    {
        public string Name { get; set; }
        public string RtsIdentifier { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CommercialParticipationDegreeResponse, CommercialParticipationDegree>();
        }
    }

    public class ManualUploadMethodReason : IMapFrom<ManualUploadMethodReasonResponse>
    {
        public object UploadMethodReason { get; set; }
        public object RtsIdentifier { get; set; }
        public object UploadMethodReasonNote { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ManualUploadMethodReasonResponse, ManualUploadMethodReason>();
        }
    }

    public class StudyConsumerParticipationDegreeElement : IMapFrom<StudyConsumerParticipationDegreeElementResponse>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string RtsIdentifier { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyConsumerParticipationDegreeElementResponse, StudyConsumerParticipationDegreeElement>();
        }
    }

    public class StudyDesign : IMapFrom<StudyDesignResponse>
    {
        public CommercialParticipationDegree StudyDesignType { get; set; }
        public string InterventionDescription { get; set; }
        public object ObervationalDetail { get; set; }
        public List<CommercialParticipationDegree> Interventional { get; set; }
        public List<CommercialParticipationDegree> InterventionalDetail { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyDesignResponse, StudyDesign>();
        }
    }

    public class StudyDisease : IMapFrom<StudyDiseaseResponse>
    {
        public CommercialParticipationDegree Disease { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyDiseaseResponse, StudyDisease>();
        }
    }

    public class StudyHrc : IMapFrom<StudyHrcResponse>
    {
        public CommercialParticipationDegree Category { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyHrcResponse, StudyHrc>();
        }
    }

    public class StudyOrganisation : IMapFrom<StudyOrganisationResponse>
    {
        public string Name { get; set; }
        public string RtsIdentifier { get; set; }
        public CommercialParticipationDegree OrgRoleName { get; set; }
        public List<StudyFunding> StudyFundings { get; set; }
        public List<StudySite> StudySites { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyOrganisationResponse, StudyOrganisation>();
        }
    }

    public class StudyFunding : IMapFrom<StudyFundingResponse>
    {
        public string Name { get; set; }
        public string GrantCode { get; set; }
        public string RtsIdentifier { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyFundingResponse, StudyFunding>();
        }
    }

    public class StudySite : IMapFrom<StudySiteResponse>
    {
        public string RtsIdentifier { get; set; }
        public string Name { get; set; }
        public object NetworkRecruitmentTarget { get; set; }
        public DateTimeOffset? PlannedRecruitmentStartDate { get; set; }
        public DateTimeOffset? PlannedRecruitmentEndDate { get; set; }
        public DateTimeOffset? ActualRecruitmentStartDate { get; set; }
        public object ActualRecruitmentEndDate { get; set; }
        public ParentOrganisationAsSite ParentOrganisationAsSite { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudySiteResponse, StudySite>();
        }
    }

    public class ParentOrganisationAsSite : IMapFrom<ParentOrganisationAsSiteResponse>
    {
        public object ParentOrganisationAsSiteReason { get; set; }
        public object ParentOrganisationAsSiteReasonNote { get; set; }
        public object RtsIdentifier { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ParentOrganisationAsSiteResponse, ParentOrganisationAsSite>();
        }
    }

    public class StudyPerson : IMapFrom<StudyPersonResponse>
    {
        public string Role { get; set; }
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyPersonResponse, StudyPerson>();
        }
    }

    public class StudySpecialty : IMapFrom<StudySpecialtyResponse>
    {
        public bool? IsManagingSpecialty { get; set; }
        public string Specialty { get; set; }
        public string RtsIdentifier { get; set; }
        public List<StudySpecialtySubspecialty> StudySpecialtySubspecialties { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudySpecialtyResponse, StudySpecialty>();
        }
    }

    public class StudySpecialtySubspecialty : IMapFrom<StudySpecialtySubspecialtyResponse>
    {
        public bool? IsPrimary { get; set; }
        public string SubSpecialty { get; set; }
        public string RtsIdentifier { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudySpecialtySubspecialtyResponse, StudySpecialtySubspecialty>();
        }
    }
}