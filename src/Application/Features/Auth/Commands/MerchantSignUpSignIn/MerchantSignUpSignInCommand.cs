using Domain.Constants;
using MediatR;

namespace Application.Features.Auth.Commands.MerchantSignUpSignIn;

public record MerchantSignUpSignInCommand(string PhoneNumber) : IRequest<MerchantStatus>;