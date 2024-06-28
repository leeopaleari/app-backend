using Application.DTOs;
using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Service;

public class ProductService(IMediator mediator, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<ProductDTO>> GetAll()
    {
        var productsQuery = new GetProductsQuery();

        if (productsQuery is null)
            throw new ApplicationException($"Entity could not be loaded.");


        var result = await mediator.Send(productsQuery);

        return mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<ProductDTO> GetById(int? id)
    {
        var productsQuery = new GetProductByIdQuery(id.Value);

        if (productsQuery is null)
            throw new ApplicationException($"Entity could not be loaded.");

        var result = await mediator.Send(productsQuery);

        return mapper.Map<ProductDTO>(result);
    }

    public async Task Add(ProductDTO productDto)
    {
        var productCreateCommand = mapper.Map<ProductCreateCommand>(productDto);
        await mediator.Send(productCreateCommand);
    }

    public async Task Update(ProductDTO productDto)
    {
        var productUpdateCommand = mapper.Map<ProductCreateCommand>(productDto);
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