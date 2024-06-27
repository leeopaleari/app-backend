using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int? id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> CreateAsync(Product entity)
    {
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Product> RemoveAsync(Product entity)
    {
        _context.Products.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Product> GetProductCategoryAsync(int? id)
    {
        // Eager loading
        return await _context.Products
            .Include(c => c.Category)
            .SingleOrDefaultAsync(p => p.Id == id);
    }
}