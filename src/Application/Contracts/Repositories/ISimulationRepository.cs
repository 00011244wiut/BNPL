using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling simulation entities.
public interface ISimulationRepository : IGenericRepository<SimulationEntity>
{
    // Asynchronously retrieves a simulation entity by phone number.
    Task<SimulationEntity?> GetSimulationByPhoneNumberAsync(string phoneNumber);
}