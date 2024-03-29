// Importing necessary namespaces and contracts
using Application.Contracts;
using Application.Contracts.Repositories;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;


// Unit of Work implementation responsible for managing repositories and transactions
public class UnitOfWork : IUnitOfWork
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize UnitOfWork with database context
    public UnitOfWork(ProjectDbContext context)
    {
        _context = context;
    }
    
    // Property to get TokenRepository instance
    public ITokenRepository TokenRepository => new TokenRepository(_context);
    
    // Property to get UserRepository instance
    public IUserRepository UserRepository => new UserRepository(_context);
    
    // Property to get UserDocumentsRepository instance
    public IUserDocumentsRepository UserDocumentsRepository => new UserDocumentsRepository(_context);
    
    // Property to get SimulationRepository instance
    public ISimulationRepository SimulationRepository => new SimulationRepository(_context);
    
    // Property to get SchedulesRepository instance
    public ISchedulesRepository SchedulesRepository => new SchedulesRepository(_context);
    
    // Property to get SalesRepository instance
    public ISalesRepository SalesRepository => new SalesRepository(_context);
    
    // Property to get PurchaseRepository instance
    public IPurchaseRepository PurchaseRepository => new PurchaseRepository(_context);
    
    // Property to get PurchaseLimitRepository instance
    public IPurchaseLimitRepository PurchaseLimitRepository => new PurchaseLimitRepository(_context);
    
    // Property to get ProductsRepository instance
    public IProductsRepository ProductsRepository => new ProductsRepository(_context);
    
    // Property to get PaymentsRepository instance
    public IPaymentsRepository PaymentsRepository => new PaymentsRepository(_context);
    
    // Property to get MerchantRepository instance
    public IMerchantRepository MerchantRepository => new MerchantRepository(_context);
    
    // Property to get MerchantDocumentsRepository instance
    public IMerchantDocumentsRepository MerchantDocumentsRepository => new MerchantDocumentsRepository(_context);
    
    // Property to get LegalDataRepository instance
    public ILegalDataRepository LegalDataRepository => new LegalDataRepository(_context);
    
    // Property to get CardsRepository instance
    public ICardsRepository CardsRepository => new CardsRepository(_context);
    
    // Property to get BankInfoRepository instance
    public IBankInfoRepository BankInfoRepository => new BankInfoRepository(_context);
    
    // Method to commit changes to the database asynchronously
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    // Method to dispose the database context
    public void Dispose()
    {
        _context.Dispose();
    }
}
