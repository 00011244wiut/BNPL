using Domain.Constants;
using MediatR;

namespace Application.Features.Auth.Commands.SignUpSignIn;

public record SignUpSignInCommand(string PhoneNumber) : IRequest<UserState>;