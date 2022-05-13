using Application.Users;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsAgreed, opt => opt.MapFrom(src => src.IsAgreed))
                .ForMember(dest => dest.SectorOptions, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore());
        }
    }
}
