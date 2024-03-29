using Domain.Constants;

namespace Application.DTOs.Auth;

// Data transfer object (DTO) representing a response for a merchant.
public record MerchantResponseDto(
    Guid? Id,
    string? FirstName,
    string? LastName,
    MerchantStatus MerchantStatus
);