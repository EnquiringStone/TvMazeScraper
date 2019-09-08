using AutoMapper;
using TvMazeScraper.Common.Data.Entities;
using TvMazeScraper.Scraper.Models.TvMaze;

namespace TvMazeScraper.Scraper.MappingProfiles
{
    public class ShowMappingProfile : Profile
    {
        public ShowMappingProfile()
        {
            CreateMap<ShowModel, Show>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CastMembers, opt => opt.Ignore());
        }
    }
}
