using Application.DTOs.Product;
using Application.DTOs.Product.Request;
using Application.Features.Products.Commands;
using AutoMapper;

namespace Application.Mappings;

public class DtoToCommandMappingProfile : Profile
{
    public DtoToCommandMappingProfile()
    {
        CreateMap<CreateProductRequest, ProductCreateCommand>();
        CreateMap<CreateProductRequest, ProductUpdateCommand>();
        CreateMap<CreateProductRequest, ProductUpdateCommand>();
    }
}