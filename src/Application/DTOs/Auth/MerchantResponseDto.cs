using Domain.Constants;

namespace Application.DTOs.Auth;

public record MerchantResponseDto(
    Guid? Id,
    string? FirstName,
    string? LastName,
    MerchantStatus MerchantStatus
);