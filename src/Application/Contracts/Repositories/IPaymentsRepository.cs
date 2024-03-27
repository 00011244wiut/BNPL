using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IPaymentsRepository : IGenericRepository<PaymentsEntity>
{
    Task<PaymentsEntity?> GetPaymentByPurchaseIdAsync(Guid purchaseId);
}