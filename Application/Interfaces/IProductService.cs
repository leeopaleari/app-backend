using Application.DTOs.Product;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ReadProductDto>> GetAll();

    Task<ReadProductDto> GetById(int? id);
    
    Task Add(CreateProductDto productDto);

    Task Update(CreateProductDto productDto);

    Task Remove(int? id);
}