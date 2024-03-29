using Domain.Constants;

namespace Application.DTOs.Auth;

// Data transfer object (DTO) representing a response for a user.
public record UserResponseDto(
    Guid? Id,
    string? FirstName,
    string? LastName,
    UserState UserState
);