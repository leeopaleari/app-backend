using Application.DTOs.Product.Request;
using Application.DTOs.Product.Response;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAll();

    Task<ProductResponse> GetById(int? id);
    
    Task Add(CreateProductRequest productRequest);

    Task Update(CreateProductRequest productRequest);

    Task Remove(int? id);
}