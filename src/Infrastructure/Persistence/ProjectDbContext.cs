using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) {  }
    
    public required DbSet<TokenEntity> Tokens { get; set; }
    public required DbSet<UserEntity> Users { get; set; }
    public required DbSet<SimulationEntity> Simulations { get; set; }
    public required DbSet<SchedulesEntity> Schedules { get; set; }
    public required DbSet<SalesEntity> Sales { get; set; }
    public required DbSet<PurchaseEntity> Purchases { get; set; }
    public required DbSet<PurchaseLimitEntity> PurchaseLimits { get; set; }
    public required DbSet<ProductsEntity> Products { get; set; }
    public required DbSet<PaymentsEntity> Payments { get; set; }
    public required DbSet<MerchantEntity> Merchants { get; set; }
    public required DbSet<MerchantDocumentsEntity> MerchantDocuments { get; set; }
    public required DbSet<LegalDataEntity> LegalData { get; set; }
    public required DbSet<CardsEntity> Cards { get; set; }
    public required DbSet<BankInfoEntity> BankInfo { get; set; }
}