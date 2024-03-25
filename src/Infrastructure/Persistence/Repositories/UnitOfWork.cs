using Application.Contracts;
using Application.Contracts.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProjectDbContext _context;
    
    public UnitOfWork(ProjectDbContext context)
    {
        _context = context;
    }
    
    public ITokenRepository TokenRepository => new TokenRepository(_context);
    public IUserRepository UserRepository => new UserRepository(_context);
    public IUserDocumentsRepository UserDocumentsRepository => new UserDocumentsRepository(_context);
    public ISimulationRepository SimulationRepository => new SimulationRepository(_context);
    public ISchedulesRepository SchedulesRepository => new SchedulesRepository(_context);
    public ISalesRepository SalesRepository => new SalesRepository(_context);
    public IPurchaseRepository PurchaseRepository => new PurchaseRepository(_context);
    public IPurchaseLimitRepository PurchaseLimitRepository => new PurchaseLimitRepository(_context);
    public IProductsRepository ProductsRepository => new ProductsRepository(_context);
    public IPaymentsRepository PaymentsRepository => new PaymentsRepository(_context);
    public IMerchantRepository MerchantRepository => new MerchantRepository(_context);
    public IMerchantDocumentsRepository MerchantDocumentsRepository => new MerchantDocumentsRepository(_context);
    public ILegalDataRepository LegalDataRepository => new LegalDataRepository(_context);
    public ICardsRepository CardsRepository => new CardsRepository(_context);
    public IBankInfoRepository BankInfoRepository => new BankInfoRepository(_context);
    
    
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}