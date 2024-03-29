using Domain.Constants;
using MediatR;

namespace Application.Features.Auth.Commands.SignUpSignIn;

// Command for signing up/signing in
public record SignUpSignInCommand(string PhoneNumber) : IRequest<UserState>;