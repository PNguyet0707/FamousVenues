
using AutoMapper;
using DataLayer.Dtos.Request;
using DataLayer.Entities;

namespace ServiceLayer.AutoMapper
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<DishCreationRequest, Dish>();
        }
    }
}
