// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for MerchantDocumentsEntity
public class MerchantDocumentsRepository : GenericRepository<MerchantDocumentsEntity>, IMerchantDocumentsRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize MerchantDocumentsRepository with database context
    public MerchantDocumentsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}