using Application.DTOs.Product;
using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Service;

public class ProductService(IMediator mediator, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<ReadProductDto>> GetAll()
    {
        var productsQuery = new GetProductsQuery();

        if (productsQuery is null)
            throw new ApplicationException($"Entity could not be loaded.");


        var result = await mediator.Send(productsQuery);

        return mapper.Map<IEnumerable<ReadProductDto>>(result);
    }

    public async Task<ReadProductDto> GetById(int? id)
    {
        var productsQuery = new GetProductByIdQuery(id.Value);

        if (productsQuery is null)
            throw new ApplicationException($"Entity could not be loaded.");

        var result = await mediator.Send(productsQuery);

        return mapper.Map<ReadProductDto>(result);
    }

    public async Task Add(CreateProductDto productDto)
    {
        var productCreateCommand = mapper.Map<ProductCreateCommand>(productDto);
        await mediator.Send(productCreateCommand);
    }

    public async Task Update(CreateProductDto productDto)
    {
        var productUpdateCommand = mapper.Map<ProductUpdateCommand>(productDto);
        await mediator.Send(productUpdateCommand);
    }

    public async Task Remove(int? id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id.Value);

        if (productRemoveCommand is null)
            throw new ApplicationException($"Entity could not be loaded.");

        await mediator.Send(productRemoveCommand);
    }
}