// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for PaymentsEntity
public class PaymentsRepository : GenericRepository<PaymentsEntity>, IPaymentsRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize PaymentsRepository with database context
    public PaymentsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    // Method to get a payment entity by purchase ID asynchronously
    public async Task<PaymentsEntity?> GetPaymentByPurchaseIdAsync(Guid purchaseId)
    {
        return await _context.Payments.FirstOrDefaultAsync(x => x.PurchaseId == purchaseId);
    }
}