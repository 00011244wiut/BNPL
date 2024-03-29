namespace Application.DTOs.Merchant;

// Data transfer object (DTO) representing merchant information for registration.
public record RegisterMerchantInfoDto(string CompanyName, string City, string TaxPayerId);