using Domain.Entities;

namespace Application.Contracts;

public interface ITokenService
{
    string GenerateAccessToken(UserEntity user);
    bool ValidateAccessToken(string token, out Guid userId);
    (Guid, string) GenerateRefreshToken();
    bool ValidateToken(string token, out Guid tokenId);
    string GenerateVerificationToken(UserEntity user);
}
