using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
}