using Application.Features.Auth.Commands.SignUpSignIn;
using Application.Features.Auth.Commands.VerifyOtp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> SignUpSignIn(string phoneNumber)
    {
        var response = await _mediator.Send(new SignUpSignInCommand(phoneNumber));
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> VerifyOTP(string phoneNumber, string sampleOtp)
    {
        var response = await _mediator.Send(new VerifyOtpCommand(phoneNumber, sampleOtp));
        return Ok(response);
    }
}