using AutoMapper;
using TvMazeScraper.ApiClient.Models;
using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.ApiClient.MappingProfiles
{
    public class ShowMappingProfile : Profile
    {
        public ShowMappingProfile()
        {
            CreateMap<Show, ShowModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src.CastMembers));
        }
    }
}
