// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for managing users
public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize UserRepository with database context
    public UserRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    // Method to retrieve a user by phone number asynchronously
    public async Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}