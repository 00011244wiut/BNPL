using Domain.Constants;
using MediatR;

namespace Application.Features.Auth.Commands.MerchantSignUpSignIn;

// Command for merchant sign-up/sign-in.
public record MerchantSignUpSignInCommand(string PhoneNumber) : IRequest<MerchantStatus>;