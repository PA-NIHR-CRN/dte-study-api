using System;
using System.Collections.Generic;
using Application.Mappings.AutoMapper;
using Application.Models.Researchers;
using AutoMapper;
using Domain.Entities.StudyRegistrations;

namespace Application.Models.StudyRegistrations
{
    public class StudyRegistrationModel : IMapFrom<IEnumerable<StudyRegistration>>
    {
        public long StudyId { get; set; }
        public string Title { get; set; }
        public ResearcherModel Researcher { get; set; }
        public string StudyRegistrationStatus { get; set; }
        public DateTime? ApprovedAtUtc { get; set; }
        public DateTime SubmittedAtUtc { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyRegistration, StudyRegistrationModel>()
                .ForMember(x => x.StudyRegistrationStatus, opt => opt.MapFrom(source => Enum.GetName(typeof(StudyRegistrationStatus), source.StudyRegistrationStatus)))
                .ForMember(x => x.Researcher, opt => opt.MapFrom(source => new ResearcherModel
                {
                    Id = source.ResearcherId,
                    Firstname = source.ResearcherFirstname,
                    Lastname = source.ResearcherLastname,
                    Email = source.ResearcherEmail
                }));
        }
    }
}