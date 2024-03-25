using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class LegalDataRepository : GenericRepository<LegalDataEntity>, ILegalDataRepository
{
    private readonly ProjectDbContext _context;
    
    public LegalDataRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}

// @LegalData