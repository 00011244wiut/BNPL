using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class SimulationRepository : GenericRepository<SimulationEntity>, ISimulationRepository
{
    private readonly ProjectDbContext _context;
    
    public SimulationRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SimulationEntity?> GetSimulationByPhoneNumberAsync(string phoneNumber)
    {
        // Get Phone Number and SampleOtp from the database
        return await _context.Simulations.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}

// @Simulation