using System;
using System.Collections.Generic;
using Application.Mappings.AutoMapper;
using Application.Models.Researchers;
using AutoMapper;
using Domain.Entities.Studies;

namespace Application.Models.Studies
{
    public class StudyModel : IMapFrom<Study>
    {
        public long StudyId { get; set; }
        public long CpmsId { get; set; }
        public string IsrctnId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public string About { get; set; }
        public string HowHelp { get; set; }
        public string WhenNeeded { get; set; }
        public string StudyQuestionnaireLink { get; set; }
        public ResearcherModel LeadResearcher { get; set; }
        public IEnumerable<StudySiteModel> Sites { get; set; }
        // public IEnumerable<EligibilityCriterionModel> EligibilityCriteria { get; set; }
        // public IEnumerable<PreScreenerQuestionModel> PreScreenerQuestions { get; set; }
        // public IEnumerable<StudyTaskModel> Tasks { get; set; }
        // public IEnumerable<StudyNotificationModel> Notifications { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedById { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string UpdatedById { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Study, StudyModel>()
                .ForMember(x => x.Status, opt => { opt.MapFrom(source => Enum.GetName(typeof(StudyStatus), source.Status)); })
                .ForMember(x => x.LeadResearcher, opt =>
                {
                    opt.MapFrom(source => new ResearcherModel
                    {
                        Id = source.LeadResearcherId,
                        Firstname = source.LeadResearcherFirstname,
                        Lastname = source.LeadResearcherLastname,
                        Email = source.LeadResearcherEmail
                    });
                });
        }
    }
}