using Application.DTOs;
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
        CreateMap<Category, CategoryDTO>().ReverseMap();
    }
}