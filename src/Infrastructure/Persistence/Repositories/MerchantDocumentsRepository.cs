using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class MerchantDocumentsRepository : GenericRepository<MerchantDocumentsEntity>, IMerchantDocumentsRepository
{
    private readonly ProjectDbContext _context;
    
    public MerchantDocumentsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}