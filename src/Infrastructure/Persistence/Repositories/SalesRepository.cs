using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class SalesRepository : GenericRepository<SalesEntity>, ISalesRepository
{
    private readonly ProjectDbContext _context;
    
    public SalesRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}

// @Sales