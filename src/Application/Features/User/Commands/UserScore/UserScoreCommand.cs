using Application.DTOs.Auth;
using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.UserScore;

public record UserScoreCommand(decimal Income, Guid UserId) : IRequest<UserResponseDto>;