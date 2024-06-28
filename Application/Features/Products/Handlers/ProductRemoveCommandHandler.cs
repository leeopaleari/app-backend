using Application.Features.Products.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Products.Handlers;

public class ProductRemoveCommandHandler(IProductRepository productRepository)
    : IRequestHandler<ProductRemoveCommand, Product>
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

    public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product is null)
        {
            throw new ApplicationException($"Entity could not be found.");
        }

        return await _productRepository.RemoveAsync(product);
    }
}