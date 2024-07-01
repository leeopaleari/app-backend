using Application.DTOs;
using Application.DTOs.Category;
using Application.DTOs.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, ReadProductDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, ReadCategoryDto>().ReverseMap();
    }
}