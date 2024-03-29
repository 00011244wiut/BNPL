using Domain.Entities;

namespace Application.Contracts;

// Interface for a token service providing methods to generate, validate, and manage tokens.
public interface ITokenService
{
    // Generates an access token for a user.
    // Parameters:
    //   user: The user entity for whom the token is generated.
    // Returns:
    //   A string representing the generated access token.
    string GenerateAccessToken(UserEntity user);

    // Generates an access token for a merchant.
    // Parameters:
    //   user: The merchant entity for whom the token is generated.
    // Returns:
    //   A string representing the generated access token.
    string GenerateMerchantAccessToken(MerchantEntity user);

    // Validates an access token.
    // Parameters:
    //   token: The access token to validate.
    //   userId: The user ID associated with the token (output parameter).
    // Returns:
    //   A boolean indicating whether the token is valid.
    bool ValidateAccessToken(string token, out Guid userId);

    // Generates a refresh token and its associated user ID.
    // Returns:
    //   A tuple containing the generated refresh token's user ID and the token itself.
    (Guid, string) GenerateRefreshToken();

    // Validates a token.
    // Parameters:
    //   token: The token to validate.
    //   tokenId: The ID associated with the token (output parameter).
    // Returns:
    //   A boolean indicating whether the token is valid.
    bool ValidateToken(string token, out Guid tokenId);

    // Generates a verification token for a user.
    // Parameters:
    //   user: The user entity for whom the verification token is generated.
    // Returns:
    //   A string representing the generated verification token.
    string GenerateVerificationToken(UserEntity user);
}