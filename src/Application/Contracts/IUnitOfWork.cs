using Application.Contracts.Repositories;

namespace Application.Contracts;

public interface IUnitOfWork : IDisposable
{
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
    
    Task CommitAsync();

}