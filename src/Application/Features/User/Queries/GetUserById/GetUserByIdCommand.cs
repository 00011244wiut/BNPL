using Application.DTOs.Auth;
using Application.DTOs.Card;
using MediatR;

namespace Application.Features.User.Queries.GetUserById;

public record GetUserByIdCommand(Guid Id) : IRequest<(UserResponseDto, CardResponseDto)>;