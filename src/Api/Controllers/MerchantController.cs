using Api.Service;
using Application.DTOs.BankInfo;
using Application.DTOs.LegalData;
using Application.DTOs.Merchant;
using Application.Features.Auth.Commands.MerchantSignUpSignIn;
using Application.Features.Auth.Commands.MerchantVerifyOtp;
using Application.Features.LegalData.ConfirmLegalData;
using Application.Features.Merchant.Commands.RegisterBankInfo;
using Application.Features.Merchant.Commands.RegisterMerchantInfo;
using Application.Features.Merchant.Commands.RegisterMerchantName;
using Application.Features.Merchant.Commands.UploadDocument;
using Application.Features.Merchant.Queries.GetMerchantById;
using Application.Features.Merchant.Queries.GetMerchantByTaxPayerId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class MerchantController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public MerchantController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("merchant/otp/[action]")]
    public async Task<IActionResult> request(string phoneNumber)
    {
        await _mediator.Send(new MerchantSignUpSignInCommand(phoneNumber));
        return Ok(new
        {
            Success = true,
            Message = "OTP sent successfully",
            Data = new
            {
                SampleOtp = "1234"
            }
        });
    }
    
    [HttpPost]
    [Route("merchant/otp/[action]")]
    public async Task<IActionResult> verify(string phoneNumber, string sampleOtp)
    {
        var response = await _mediator.Send(new MerchantVerifyOtpCommand(phoneNumber, sampleOtp));
        return Ok(new
        {
            Success = true,
            Message = "OTP verified, Merchant state updated successfully",
            Data = response
        });
    }
    
    [HttpPost]
    [Route("merchant/name/[action]")]
    public async Task<IActionResult> submit(string firstName, string lastName)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var response = await _mediator.Send(new RegisterMerchantNameCommand(firstName, lastName, userId));
        return Ok(new
        {
            Success = true,
            Message = "Name saved successfully",
            Data = response
        });
    }
    
    [HttpPost]
    [Route("merchant/info/[action]")]
    public async Task<IActionResult> submit(string companyName, string city, string taxPayerId)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var mockLegalData = await _mediator.Send(new RegisterMerchantInfoCommand(new RegisterMerchantInfoDto(companyName, city, taxPayerId),
            userId));
        return Ok(new
        {
            Success = true,
            Message = "Profile information saved successfully",
            data = mockLegalData
        });
    }
    
    [HttpPost]
    [Route("merchant/legalinfo/[action]")]
    public async Task<IActionResult> confirm(LegalDataRequestDto legalDataRequestDto)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var response = await _mediator.Send(new ConfirmLegalDataCommand(legalDataRequestDto, userId));
        return Ok(new
        {
            Success = true,
            Message = "Legal information saved successfully",
            Data = response
        });
    }

    [HttpPost]
    [Route("merchant/bank/submit")]
    public async Task<IActionResult> SubmitBankInfo(string mfo, string accountNumber)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        await _mediator.Send(new RegisterBankInfoCommand( new RegisterBankInfoDto(mfo, accountNumber), userId));
        
        return Ok(new
        {
            Success = true,
            Message = "Bank information saved successfully"
        });
    }
    
    [HttpPost]
    [Route("merchant/document/[action]")]
    public async Task<IActionResult> upload([FromForm] UploadDocumentDto uploadDocumentDto)
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var response = await _mediator.Send(new UploadDocumentCommand(uploadDocumentDto, userId));
        
        return Ok(new
        {
            Success = true,
            Message = "Document uploaded successfully",
            Data = response
        });
    }
    
    [HttpGet]
    [Route("merchant/info")]
    public async Task<IActionResult> GetMerchantInfo()
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var response = await _mediator.Send(new GetMerchantCommand(userId));
        
        return Ok(new
        {
            Success = true,
            Message = "Merchant info retrieved successfully",
            Data = response
        });
    }
    
    [HttpGet]
    [Route("merchant/taxpayer/info{id}")]
    public async Task<IActionResult> GetMerchantInfo(string id)
    {
        var response = await _mediator.Send(new GetMerchantByTaxPayerIdCommand(id));
        
        return Ok(new
        {
            Success = true,
            Message = "Merchant info retrieved successfully",
            Data = response
        });
    }

}