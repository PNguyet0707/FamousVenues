using AutoMapper;
using DataLayer.Dtos;
using DataLayer.Entities;
namespace ServiceLayer.AutoMapper
{
    public class VenueProfile : Profile
    {
        public VenueProfile()
        {
            CreateMap<Venue, VenueResponse>()
                  .ForMember(dest => dest.ImageUrl,
                   opt => opt.MapFrom(src => src.ImageName));
        }
    }
}
