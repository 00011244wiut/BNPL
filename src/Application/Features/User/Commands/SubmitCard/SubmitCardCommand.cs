using Application.DTOs.Auth;
using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.SubmitCard;

public record SubmitCardCommand(SubmitCardDto  SubmitCardDto, Guid UserId) : IRequest<UserResponseDto>;