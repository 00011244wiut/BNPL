using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling legal data entities.
public interface ILegalDataRepository : IGenericRepository<LegalDataEntity>
{
    // Asynchronously generates mock legal data based on the specified city.
    Task<LegalDataEntity> MockLegalData(string city);
}