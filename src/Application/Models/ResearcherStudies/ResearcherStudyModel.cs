using System;
using Application.Mappings.AutoMapper;
using AutoMapper;
using Domain.Entities.Studies;

namespace Application.Models.ResearcherStudies
{
    public class ResearcherStudyModel : IMapFrom<ResearcherStudy>
    {
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public long StudyId { get; set; }
        public string Role { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ResearcherStudy, ResearcherStudyModel>()
                .ForMember(x => x.Role, opt => { opt.MapFrom(source => Enum.GetName(typeof(ResearcherStudyRole), source.Role)); });
        }
    }
}