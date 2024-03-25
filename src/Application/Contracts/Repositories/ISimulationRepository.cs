using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ISimulationRepository : IGenericRepository<SimulationEntity>
{
    Task<SimulationEntity?> GetSimulationByPhoneNumberAsync(string phoneNumber);
}