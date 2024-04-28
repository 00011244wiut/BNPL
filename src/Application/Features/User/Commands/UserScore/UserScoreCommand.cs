using Application.DTOs.Auth;
using Domain.Constants; // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.User.Commands.UserScore;  // Namespace declaration

public record UserScoreCommand(int Income, int Age, Gender Gender, MaritalStatus MaritalStatus, Guid UserId) : IRequest<(UserResponseDto, decimal)>;  // Command declaration