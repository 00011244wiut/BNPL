using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling user entities.
public interface IUserRepository : IGenericRepository<UserEntity>
{
    // Asynchronously retrieves a user entity by phone number.
    Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber);
}