using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    private readonly ProjectDbContext _context;
    
    public UserRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}

// @User