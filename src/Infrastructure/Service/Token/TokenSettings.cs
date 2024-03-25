namespace Infrastructure.Service.Token;

public class TokenSettings
{
    public const string SectionName = "JwtSettings";
    public string AccessTokenSecret { get; set; } = string.Empty;
    public string RefreshTokenSecret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationInHours { get; set; }
}
