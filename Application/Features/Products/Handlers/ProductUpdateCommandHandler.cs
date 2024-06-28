using Application.Features.Products.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Products.Handlers;

public class ProductUpdateCommandHandler(IProductRepository productRepository)
    : IRequestHandler<ProductUpdateCommand, Product>
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

    public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product is null)
        {
            throw new ApplicationException($"Entity could not be found.");
        }

        product.Update(
            request.Name,
            request.Description,
            request.Price,
            request.Stock,
            request.Image,
            request.CategoryId);

        return await _productRepository.UpdateAsync(product);
    }
}