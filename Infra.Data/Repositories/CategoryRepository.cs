using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category> GetByIdAsync(int? id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category> CreateAsync(Category entity)
    {
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Category> UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Category> RemoveAsync(Category entity)
    {
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}