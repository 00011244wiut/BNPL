using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class BankInfoRepository : GenericRepository<BankInfoEntity>, IBankInfoRepository
{
    private readonly ProjectDbContext _context;
    
    public BankInfoRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}