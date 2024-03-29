using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling payment entities.
public interface IPaymentsRepository : IGenericRepository<PaymentsEntity>
{
    // Asynchronously retrieves a payment entity by purchase ID.
    Task<PaymentsEntity?> GetPaymentByPurchaseIdAsync(Guid purchaseId);
}