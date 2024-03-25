using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<UserEntity>
{
    Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber);
}