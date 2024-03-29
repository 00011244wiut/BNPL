using Application.DTOs.Auth;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.User.Commands.UserScore;  // Namespace declaration

public record UserScoreCommand(decimal Income, Guid UserId) : IRequest<UserResponseDto>;  // Command declaration