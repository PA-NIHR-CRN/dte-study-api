using Application.Mappings.AutoMapper;
using AutoMapper;
using Domain.Entities.Participants;

namespace Application.Models.Participants
{
    public class ParticipantDetailsModel : IMapFrom<ParticipantDetails>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string SessionId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ParticipantDetails, ParticipantDetailsModel>();
        }
    }
}