using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class PaymentsRepository : GenericRepository<PaymentsEntity>, IPaymentsRepository
{
    private readonly ProjectDbContext _context;
    
    public PaymentsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}

// @Payments