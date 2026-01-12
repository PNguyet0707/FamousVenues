using AutoMapper;
using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;
using DataLayer.Entities;

namespace ServiceLayer.AutoMapper
{
    public class ChefProfile : Profile
    {
        public ChefProfile()
        {
            CreateMap<ChefCreationRequest, Chef>();
            CreateMap<Chef, ChefCreationResponse>();
        }
    }
}