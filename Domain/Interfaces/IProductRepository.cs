using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product> GetProductCategoryAsync(int? id);
}