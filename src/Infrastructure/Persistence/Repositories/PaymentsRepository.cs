using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PaymentsRepository : GenericRepository<PaymentsEntity>, IPaymentsRepository
{
    private readonly ProjectDbContext _context;
    
    public PaymentsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PaymentsEntity?> GetPaymentByPurchaseIdAsync(Guid purchaseId)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(x => x.PurchaseId == purchaseId);
    }
}

// @Payments