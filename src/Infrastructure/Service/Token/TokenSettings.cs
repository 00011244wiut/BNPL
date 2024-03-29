// Namespace for Token service implementation
namespace Infrastructure.Service.Token;

// Class to hold token settings
public class TokenSettings
{
    // Constant to represent the section name in configuration
    public const string SectionName = "JwtSettings";
    
    // Property to store access token secret
    public string AccessTokenSecret { get; set; } = string.Empty;
    
    // Property to store refresh token secret
    public string RefreshTokenSecret { get; set; } = string.Empty;
    
    // Property to store issuer
    public string Issuer { get; set; } = string.Empty;
    
    // Property to store audience
    public string Audience { get; set; } = string.Empty;
    
    // Property to store expiration period in hours
    public int ExpirationInHours { get; set; }
}