using AutoMapper;
using e_Commerce.Data.Entities;
using e_Commerce.Services.Requests;

namespace e_Commerce_Website
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserRequest, User>();
            CreateMap<ProductRequest, Product>();
        }
    }
}
