// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for BankInfoEntity
public class BankInfoRepository : GenericRepository<BankInfoEntity>, IBankInfoRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize BankInfoRepository with database context
    public BankInfoRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}