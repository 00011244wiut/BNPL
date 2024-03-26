using Application.Features.Auth.Commands.SignUpSignIn;
using Application.Features.Auth.Commands.VerifyOtp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("otp/[action]")]
public class OtpController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public OtpController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> request(string phoneNumber)
    {
        var response = await _mediator.Send(new SignUpSignInCommand(phoneNumber));
        return Ok(new
        {
            Success = true,
            Message = "OTP sent successfully"
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> verify(string phoneNumber, string sampleOtp)
    {
        var response = await _mediator.Send(new VerifyOtpCommand(phoneNumber, sampleOtp));
        return Ok(new
        {
            Success = true,
            Message = "OTP verified, user state updated successfully",
            Data = response
        });
    }
}