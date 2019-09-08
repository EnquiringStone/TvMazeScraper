using AutoMapper;
using TvMazeScraper.ApiClient.Models;
using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.ApiClient.MappingProfiles
{
    public class CastMemberMappingProfile : Profile
    {
        public CastMemberMappingProfile()
        {
            CreateMap<CastMember, CastMemberModel>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Person.DateOfBirth))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Person.ExternalId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name));
        }
    }
}
