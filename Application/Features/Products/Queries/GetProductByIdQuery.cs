using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }

    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}