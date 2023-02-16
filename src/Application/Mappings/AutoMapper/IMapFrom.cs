using AutoMapper;

namespace Application.Mappings.AutoMapper
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}