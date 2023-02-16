using System;
using Application.Mappings.AutoMapper;
using AutoMapper;
using Domain.Entities.Studies.Sites;

namespace Application.Models.Studies
{
    public class StudySiteModel : IMapFrom<StudySite>
    {
        public string Id { get; set; }
        public string RtsIdentifier { get; set; }
        public long StudyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SiteAddressModel Address { get; set; }
        public string Status { get; set; }
        
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedById { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string UpdatedById { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SiteAddress, SiteAddressModel>();
            profile.CreateMap<StudySite, StudySiteModel>()
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address));
        }
    }
}