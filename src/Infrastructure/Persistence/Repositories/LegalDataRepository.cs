// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;


// Repository implementation for LegalDataEntity
public class LegalDataRepository : GenericRepository<LegalDataEntity>, ILegalDataRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize LegalDataRepository with database context
    public LegalDataRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    // Method to mock legal data for a given city
    public Task<LegalDataEntity> MockLegalData(string city)
    {
        // Generating random legal data
        var (businessType, legalName, legalAddress, directorName) = GetRandomLegalData();
        
        // Creating mock legal data entity
        var mockData = new LegalDataEntity
        {
            BusinessType = businessType,
            LegalName = legalName,
            LegalAddress = $"{city} {legalAddress}",
            DirectorName = directorName,
            City = city
        };

        // Returning the mock legal data entity
        return Task.FromResult(mockData);
    }

    // Method to get random legal data details
    private static (string BusinessType, string LegalName, string LegalAddress, string DirectorName) GetRandomLegalData()
    {
        // Business Types
        List<string> businessTypes = new List<string>
        {
            "Retail", "E-commerce", "Service-Based", "Manufacturing", "Wholesale",
            "Franchise", "Food and Beverage", "Hospitality", "Consulting", "Technology",
            "Healthcare", "Real Estate", "Transportation", "Financial Services", "Entertainment",
            "Construction", "Fitness and Wellness", "Education", "Nonprofit", "Legal Services",
            "Marketing and Advertising", "Travel and Tourism", "Cleaning and Maintenance", "Automotive",
            "Environmental Services", "Event Planning", "Pet Care", "Art and Design", "Photography", "Agriculture"
        };

        // Director Names
        List<string> directors = new List<string>
        {
            "Alice Archer", "Benjamin Blackwood", "Cassandra Cruz", "Dylan Drake", "Emily Everest",
            "Finn Fitzgerald", "Grace Greenwood", "Harrison Hunter", "Isabella Ivory", "Jasper Justice",
            "Kennedy Kingston", "Luna Lexington", "Mason Montgomery", "Nova Nightingale", "Oliver Orion",
            "Penelope Parker", "Quinn Quincy", "Ruby Raines", "Sebastian Sterling", "Talia Trueheart",
            "Ulysses Upton", "Violet Valentine", "William Wilde", "Xander Xavier", "Yasmin York", "Zachary Zephyr"
        };

        // Legal Names
        List<string> names = new List<string>
        {
            "Retail Rendezvous", "Cyber Commerce Hub", "Service Synergy Solutions", "Artisan Alchemy Works", "Bulk Bounty Enterprise",
            "Franchise Fusion Corp", "Gastronomic Gala Inc", "Hospitality Haven Holdings", "Sage Strategic Consulting", "Techno Trance Tech",
            "Health Harmony Healers", "Realty Realm Ventures", "Transit Trek Logistics", "Fortune Forge Financials", "Stellar Showbiz Studios",
            "Constructa Craft Conclave", "Fit Fusion Fitness", "Edu Enlighten Emporium", "Heartfelt Help Foundation", "Juris Junction Law Firm",
            "Market Magnate Media", "Wander Wonder Worldwide", "Pure Path Cleaning", "Auto Assemble Automotives", "Eco Echo Solutions",
            "Event Empire Enterprises", "Pawsome Pampering Palace", "Artistic Avenue Agency", "Capture Craze Creations", "Agri Artisan Alliance"
        };

        // Legal Addresses
        List<string> addresses = new List<string>
        {
            "Retail Road", "Cyber City Center", "Service Street Suite", "Artisan Avenue", "Bulk Boulevard",
            "Franchise Front", "Gastronomic Grove", "Hospitality Highway", "Sage Square", "Techno Tower",
            "Health Haven", "Realty Ridge", "Transit Trail", "Fortune Firmament", "Stellar Stage",
            "Constructa Corner", "Fit Fusion Plaza", "Edu Edifice", "Heartfelt House", "Juris Junction",
            "Market Mall", "Wander Way", "Pure Path Place", "Auto Alley", "Eco Estate", "Event Emporium",
            "Pawsome Point", "Artistic Arcade", "Capture Crescent", "Agri Acre"
        };
        
        // Randomly selecting an index
        var random = new Random();
        var index = random.Next(0, businessTypes.Count - 1);
        
        // Returning the corresponding legal data details
        return (businessTypes[index], names[index], addresses[index], directors[index]);
    }
}
