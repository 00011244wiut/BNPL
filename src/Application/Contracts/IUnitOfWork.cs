using Application.Contracts.Repositories;

namespace Application.Contracts;

// Interface for a unit of work representing a set of operations that should be performed as a single transaction.
public interface IUnitOfWork : IDisposable
{
    // Repositories for various entities.
    ITokenRepository TokenRepository { get; }
    IUserRepository UserRepository { get; }
    IUserDocumentsRepository UserDocumentsRepository { get; }
    ISimulationRepository SimulationRepository { get; }
    ISchedulesRepository SchedulesRepository { get; }
    ISalesRepository SalesRepository { get; }
    IPurchaseRepository PurchaseRepository { get; }
    IPurchaseLimitRepository PurchaseLimitRepository { get; }
    IProductsRepository ProductsRepository { get; }
    IPaymentsRepository PaymentsRepository { get; }
    IMerchantRepository MerchantRepository { get; }
    IMerchantDocumentsRepository MerchantDocumentsRepository { get; }
    ILegalDataRepository LegalDataRepository { get; }
    ICardsRepository CardsRepository { get; }
    IBankInfoRepository BankInfoRepository { get; }
    
    // Asynchronously commits changes made within the unit of work to the underlying data store.
    Task CommitAsync();
}