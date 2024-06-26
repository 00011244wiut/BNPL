﻿using Api.Service;
using Application.DTOs.User;
using Application.Features.User.Commands.RegisterUserInfo;
using Application.Features.User.Commands.SubmitCard;
using Application.Features.User.Commands.UploadPhotos;
using Application.Features.User.Commands.UserScore;
using Domain.Constants;
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
    public async Task<IActionResult> kyc([FromForm] UploadPicturesDto uploadDocumentDto)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var verification = await _mediator.Send(new UploadPhotosCommand(uploadDocumentDto, userId));
        return Ok(new
        {
            Success = true,
            Message = "KYC documents submitted",
            Data = verification
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
    
    [HttpPost]
    public async Task<IActionResult> scoring(int income, int age, Gender gender, MaritalStatus maritalStatus)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var (userResponseDto, maxAmount) = await _mediator.Send(new UserScoreCommand(income, age, gender, maritalStatus, userId));
        return Ok(new
        {
            Success = true,
            Message = "Scoring process completed, result saved",
            Data = new
            {
                User = userResponseDto,
                MaxAmount = maxAmount
            }
        });
    }

}