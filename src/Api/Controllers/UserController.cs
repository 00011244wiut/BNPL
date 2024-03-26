﻿using Api.Service;
using Application.DTOs.User;
using Application.Features.User.Commands.RegisterUserInfo;
using Application.Features.User.Commands.SubmitCard;
using Application.Features.User.Commands.UploadDocument;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("user/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> profile(string firstName, string lastName)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        await _mediator.Send(new RegisterUserInfoCommand(new RegisterUserInfoDto(firstName, lastName), userId));
        return Ok(new
        {
            Success = true,
            Message = "Profile information saved successfully"
        });
    }

    [HttpPost]
    public async Task<IActionResult> kyc([FromForm] UploadDocumentDto uploadDocumentDto)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        await _mediator.Send(new UploadDocumentCommand(uploadDocumentDto, userId));
        return Ok(new
        {
            Success = true,
            Message = "KYC documents submitted, pending verification"
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> card(SubmitCardDto submitCardDto)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        await _mediator.Send(new SubmitCardCommand(submitCardDto, userId));
        return Ok(new
        {
            Success = true,
            Message = "Card details saved successfully"
        });
    }

}