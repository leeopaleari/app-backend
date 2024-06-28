using Application.DTOs.Product;
using Application.Features.Products.Commands;
using AutoMapper;

namespace Application.Mappings;

public class DtoToCommandMappingProfile : Profile
{
    public DtoToCommandMappingProfile()
    {
        CreateMap<CreateProductDto, ProductCreateCommand>();
        CreateMap<CreateProductDto, ProductUpdateCommand>();
        CreateMap<CreateProductDto, ProductUpdateCommand>();
    }
}