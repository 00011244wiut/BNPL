using Application.DTOs.Auth; // Importing authentication related DTOs
using Application.DTOs.User; // Importing user related DTOs
using MediatR; // Importing MediatR for handling commands and queries

namespace Application.Features.User.Commands.SubmitCard; // Namespace declaration for the SubmitCardCommand feature

// Defining a record representing the SubmitCardCommand
public record SubmitCardCommand(SubmitCardDto  SubmitCardDto, Guid UserId) : IRequest<UserResponseDto>;
// 'SubmitCardCommand' is a request to submit a card, containing a SubmitCardDto and the UserId,
// and it returns a UserResponseDto