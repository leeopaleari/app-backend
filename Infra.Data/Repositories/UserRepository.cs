using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;

namespace Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<User> RemoveAsync(User entity)
    {
        throw new NotImplementedException();
    }
}