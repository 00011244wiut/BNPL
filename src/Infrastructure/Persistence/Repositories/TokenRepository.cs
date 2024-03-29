// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;


// Repository implementation for TokenEntity
public class TokenRepository : GenericRepository<TokenEntity>, ITokenRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize TokenRepository with database context
    public TokenRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    // Method to get a token entity by token asynchronously
    public async Task<TokenEntity?> GetByTokenAsync(string token)
    {
        return await _context.Tokens
            .Where(t => t.RefreshToken == token)
            .FirstOrDefaultAsync();
    }

    // Method to get a token entity by user ID asynchronously
    public async Task<TokenEntity?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Tokens
            .Where(t => t.UserId == userId)
            .FirstOrDefaultAsync();
    }
    
    // Method to get a token entity by merchant ID asynchronously
    public async Task<TokenEntity?> GetByMerchantIdAsync(Guid merchantId)
    {
        return await _context.Tokens
            .Where(t => t.MerchantId == merchantId)
            .FirstOrDefaultAsync();
    }
}