using Application.DTOs.Product.Request;
using Application.DTOs.Product.Response;
using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Service;

public class ProductService(IMediator mediator, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<ProductResponse>> GetAll()
    {
        var productsQuery = new GetProductsQuery();

        if (productsQuery is null)
            throw new ApplicationException($"Entity could not be loaded.");


        var result = await mediator.Send(productsQuery);

        return mapper.Map<IEnumerable<ProductResponse>>(result);
    }

    public async Task<ProductResponse> GetById(int? id)
    {
        var productsQuery = new GetProductByIdQuery(id.Value);

        if (productsQuery is null)
            throw new ApplicationException($"Entity could not be loaded.");

        var result = await mediator.Send(productsQuery);

        return mapper.Map<ProductResponse>(result);
    }

    public async Task Add(CreateProductRequest productRequest)
    {
        var productCreateCommand = mapper.Map<ProductCreateCommand>(productRequest);
        await mediator.Send(productCreateCommand);
    }

    public async Task Update(CreateProductRequest productRequest)
    {
        var productUpdateCommand = mapper.Map<ProductUpdateCommand>(productRequest);
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