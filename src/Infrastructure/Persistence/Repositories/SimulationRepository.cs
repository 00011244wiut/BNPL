// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for SimulationEntity
public class SimulationRepository : GenericRepository<SimulationEntity>, ISimulationRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize SimulationRepository with database context
    public SimulationRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    // Method to get a simulation entity by phone number asynchronously
    public async Task<SimulationEntity?> GetSimulationByPhoneNumberAsync(string phoneNumber)
    {
        // Get Phone Number and SampleOtp from the database
        return await _context.Simulations.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}