// Purpose: This file contains the ProjectDbContext class which is used to represent the database context for the project.
// It contains properties to represent each table in the database.
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ProjectDbContext : DbContext
{
    // Constructor to initialize ProjectDbContext with options
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) {  }
    
    // Property to represent Tokens table in database
    public required DbSet<TokenEntity> Tokens { get; set; }
    // Property to represent Users table in database
    public required DbSet<UserEntity> Users { get; set; }
    // Property to represent UserDocuments table in database
    public required DbSet<UserDocumentsEntity> UserDocuments { get; set; }
    // Property to represent Simulation table in database
    public required DbSet<SimulationEntity> Simulations { get; set; }
    // Property to represent Schedules table in database
    public required DbSet<SchedulesEntity> Schedules { get; set; }
    // Property to represent Sales table in database
    public required DbSet<SalesEntity> Sales { get; set; }
    // Property to represent Purchase table in database     
    public required DbSet<PurchaseEntity> Purchases { get; set; }
    // Property to represent PurchaseLimit table in database
    public required DbSet<PurchaseLimitEntity> PurchaseLimits { get; set; }
    // Property to represent Products table in database
    public required DbSet<ProductsEntity> Products { get; set; }
    // Property to represent Payments table in database
    public required DbSet<PaymentsEntity> Payments { get; set; }
    // Property to represent Merchants table in database
    public required DbSet<MerchantEntity> Merchants { get; set; }
    // Property to represent MerchantDocuments table in database
    public required DbSet<MerchantDocumentsEntity> MerchantDocuments { get; set; }
    // Property to represent LegalData table in database
    public required DbSet<LegalDataEntity> LegalData { get; set; }
    // Property to represent Cards table in database
    public required DbSet<CardsEntity> Cards { get; set; }
    // Property to represent BankInfo table in database
    public required DbSet<BankInfoEntity> BankInfo { get; set; }
    // Property to represent Address table in database
}