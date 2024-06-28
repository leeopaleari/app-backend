using Application.DTOs;
using Application.Features.Products.Commands;
using AutoMapper;

namespace Application.Mappings;

public class DtoToCommandMappingProfile : Profile
{
    public DtoToCommandMappingProfile()
    {
        CreateMap<ProductDTO, ProductCreateCommand>();
        CreateMap<ProductDTO, ProductUpdateCommand>();
        CreateMap<ProductDTO, ProductUpdateCommand>();
    }
}