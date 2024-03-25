using Domain.Constants;

namespace Application.DTOs.Auth;

public record UserResponseDto(
    Guid? Id,
    string? FirstName,
    string? LastName,
    UserState UserState
);