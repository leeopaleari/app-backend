using Application.DTOs;
using Application.Features.Products.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
    }
}