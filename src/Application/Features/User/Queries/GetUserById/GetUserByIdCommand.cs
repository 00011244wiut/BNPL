using Application.DTOs.Auth;  // Importing necessary namespaces
using Application.DTOs.Card;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.User.Queries.GetUserById;  // Namespace declaration

public record GetUserByIdCommand(Guid Id) : IRequest<(UserResponseDto, CardResponseDto)>;  // Command declaration