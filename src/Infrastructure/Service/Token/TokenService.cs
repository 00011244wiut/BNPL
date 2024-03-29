// Importing necessary namespaces and contracts
using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Application.Contracts;
using Domain.Entities;

// Namespace for Token service implementation
namespace Infrastructure.Service.Token;

// Implementation of the TokenService contract
public class TokenService : ITokenService
{
    // Fields to store access token secret, refresh token secret, issuer, and audience
    private readonly byte[] _accessTokenSecret;
    private readonly byte[] _refreshTokenSecret;
    private readonly string _issuer;
    private readonly string _audience;

    // Constructor to initialize TokenService with token settings
    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        _accessTokenSecret = Encoding.ASCII.GetBytes(tokenSettings.Value.AccessTokenSecret);
        _refreshTokenSecret = Encoding.ASCII.GetBytes(tokenSettings.Value.RefreshTokenSecret);
        _issuer = tokenSettings.Value.Issuer;
        _audience = tokenSettings.Value.Audience;
    }

    // Method to generate access token for user
    public string GenerateAccessToken(UserEntity user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.PhoneNumber),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UnixEpoch.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                    new Claim(ClaimTypes.NameIdentifier, user.FirstName ?? ""),
                    new Claim(ClaimTypes.Name, user.LastName ?? ""),
                    new Claim(ClaimTypes.Role, user.UserState.ToString()),
                }
            ),

            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_accessTokenSecret),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = _issuer,
            Audience = _audience,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    // Method to generate access token for merchant
    public string GenerateMerchantAccessToken(MerchantEntity merchant)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, merchant.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, merchant.PhoneNumber),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UnixEpoch.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                    new Claim(ClaimTypes.NameIdentifier, merchant.FirstName ?? ""),
                    new Claim(ClaimTypes.Name, merchant.LastName ?? "")
                }
            ),

            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_accessTokenSecret),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = _issuer,
            Audience = _audience,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // Method to generate refresh token
    public (Guid, string) GenerateRefreshToken()
    {
        var tokenId = Guid.NewGuid();
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(JwtRegisteredClaimNames.Jti, tokenId.ToString()), }),
            Expires = DateTime.UtcNow.AddDays(14),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_refreshTokenSecret),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = _issuer,
            Audience = _audience,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenId, tokenHandler.WriteToken(token));
    }

    // Method to validate refresh token
    public bool ValidateToken(string refreshToken, out Guid tokenId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationParams = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(_refreshTokenSecret),
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = _audience,
            ValidIssuer = _issuer,
        };

        try
        {
            tokenHandler.ValidateToken(refreshToken, tokenValidationParams, out SecurityToken token);
            var jwt = (JwtSecurityToken)token;
            var valid = Guid.TryParse(jwt.Id, out var id);
            tokenId = id;
            return valid;
        }
        catch (Exception)
        {
            tokenId = default;
            return false;
        }
    }

    // Method to generate verification token
    public string GenerateVerificationToken(UserEntity user)
    {
        var randomVal = user.Id.ToString() + user.PhoneNumber + user.LastName;
        var userHash = Convert.ToHexString(Encoding.ASCII.GetBytes(randomVal)) + Guid.NewGuid().ToString() + DateTime.UnixEpoch.ToString(CultureInfo.InvariantCulture);
        var token = SHA256.HashData(Encoding.ASCII.GetBytes(userHash));
        return Convert.ToHexString(token);
    }

    // Method to validate access token
    public bool ValidateAccessToken(string accessToken, out Guid userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationParams = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(_accessTokenSecret),
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = _audience,
            ValidIssuer = _issuer,
        };

        try
        {
            tokenHandler.ValidateToken(accessToken, tokenValidationParams, out SecurityToken token);
            var jwt = (JwtSecurityToken)token;
            var valid = Guid.TryParse(jwt.Subject, out var id);
            userId = id;
            return valid;
        }
        catch (Exception)
        {
            userId = default;
            return false;
        }
    }
}
