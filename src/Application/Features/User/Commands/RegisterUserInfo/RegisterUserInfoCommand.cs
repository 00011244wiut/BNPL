using Application.DTOs.Auth;
using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.RegisterUserInfo;

public record RegisterUserInfoCommand(RegisterUserInfoDto  RegisterUserInfoDto, Guid UserId) : IRequest<UserResponseDto>;