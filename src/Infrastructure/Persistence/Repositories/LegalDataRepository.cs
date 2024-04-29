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
            "Individual (ИП)", "Limited Liability Company (ООО)"
        };

        // Director Names
        List<string> directors = new List<string>
        {
            "Aziza Akhmedova", "Bekzod Bakirov", "Camila Choriyeva", "Dilshod Davronov", "Elina Eshonkulova",
            "Farrukh Fozilov", "Gulnora Ganieva", "Husniddin Holikov", "Iroda Ibragimova", "Jasur Jumayev",
            "Kamila Karimova", "Laziz Latipov", "Madina Mirzaeva", "Nodira Nazarova", "Otabek Olimov",
            "Polina Petrova", "Qodir Qosimov", "Rustam Rakhimov", "Saida Sobirova", "Timur Tursunov",
            "Ulugbek Umarov", "Vasilisa Vakhidova", "Wahid Wahhodov", "Xurshid Xodjayev", "Yulduz Yusupova", "Zafar Zokirov"
        };

        // Legal Names
        List<string> names = new List<string>
        {
            "Global Goods Gateway", "Cyber Silk Road Hub", "Service Samarkand Solutions", "Artisan Aral Works", "Bulk Bukhara Enterprise",
            "Franchise Ferghana Corp", "Gastronomic Gijduvon Inc", "Hospitality Khiva Holdings", "Sage Samanid Consulting", "Techno Tashkent Tech",
            "Health Hazorasp Healers", "Realty Registan Ventures", "Transit Tien Shan Logistics", "Fortune Fergana Financials", "Stellar Silk Show Studios",
            "Constructa Chust Conclave", "Fit Ferghana Fitness", "Edu Enlightenment Emporium", "Heartfelt Humo Foundation", "Juris Jizzakh Law Firm",
            "Market Margilan Media", "Wander Warraq Worldwide", "Pure Pamir Cleaning", "Auto Andijan Automotives", "Eco Eclectic Solutions",
            "Event Emir Enterprises", "Pawsome Pamukkale Palace", "Artistic Alay Agency", "Capture Chimgan Creations", "Agri Anor Alliance"
        };


        // Legal Addresses
        List<string> addresses = new List<string>
        {
            "56 Retail Road, Tashkent, 100000", "88 Cyber City Center, Samarkand, 140100", "123 Service Street Suite, Bukhara, 200100", 
            "22 Artisan Avenue, Khiva, 220900", "18 Bulk Boulevard, Ferghana, 150100", "48 Franchise Front, Namangan, 160500", 
            "77 Gastronomic Grove, Andijan, 170100", "99 Hospitality Highway, Nukus, 230113", "36 Sage Square, Jizzakh, 130100", 
            "82 Techno Tower, Urgench, 220200", "65 Health Haven, Karshi, 180200", "74 Realty Ridge, Kokand, 150800", 
            "59 Transit Trail, Navoi, 210100", "45 Fortune Firmament, Margilan, 150700", "67 Stellar Stage, Shakhrisabz, 180300", 
            "21 Constructa Corner, Gulistan, 120100", "95 Fit Fusion Plaza, Zarafshan, 210300", "33 Edu Edifice, Chirchiq, 111700", 
            "29 Heartfelt House, Bekabad, 110200", "39 Juris Junction, Chust, 160900", "91 Market Mall, Angren, 110400", 
            "84 Wander Way, Termez, 190300", "27 Pure Path Place, Syrdarya, 120200", "12 Auto Alley, Denov, 190500", 
            "43 Eco Estate, Tashkent, 100000", "38 Event Emporium, Nukus, 230114", "56 Pawsome Point, Samarkand, 140102", 
            "64 Artistic Arcade, Bukhara, 200102", "72 Capture Crescent, Khiva, 220901", "31 Agri Acre, Ferghana, 150103"
        };
        
        // Randomly selecting an index
        var random = new Random();
        
        // Returning the corresponding legal data details
        return (
            businessTypes[random.Next(0, businessTypes.Count)], 
            names[random.Next(0, names.Count)], 
            addresses[random.Next(0, addresses.Count)], 
            directors[random.Next(0, directors.Count)]
            );
    }
}
