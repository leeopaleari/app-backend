using Application.DTOs.Category;
using Application.DTOs.Product.Request;
using Application.DTOs.Product.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Product, CreateProductRequest>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, ReadCategoryDto>().ReverseMap();

        // CreateMap<RegisterUserDto, ApplicationUser>()
        //     .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
        //     .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}