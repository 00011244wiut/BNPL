using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TokenRepository : GenericRepository<TokenEntity>, ITokenRepository
{
    private readonly ProjectDbContext _context;
    public TokenRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<TokenEntity?> GetByTokenAsync(string token)
    {
        return await _context.Tokens
            .Where(t => t.RefreshToken == token)
            .FirstOrDefaultAsync();
    }

    public async Task<TokenEntity?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Tokens
            .Where(t => t.UserId == userId)
            .FirstOrDefaultAsync();
    }
}

