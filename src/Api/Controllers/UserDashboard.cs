using Api.Service;
using Application.Features.UserDashboard.Queries.GetAllPurchases;
using Application.Features.UserDashboard.Queries.GetPaymentByPurchaseId;
using Application.Features.UserDashboard.Queries.GetPurchaseById;
using Application.Features.UserDashboard.Queries.GetUserInfo;
using Application.Features.UserDashboard.Queries.GetUserLimit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class UserDashboard : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserDashboard(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("user/[action]")]
    public async Task<IActionResult> purchases()
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var purchases = await _mediator.Send(new GetAllPurchaseCommand(userId));
        
        return Ok(new
        {
            Success = true,
            Message = "Purchases fetched successfully",
            Data = purchases
        });
    }

    [HttpGet]
    [Route("user/[action]")]
    public async Task<IActionResult> info()
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var (firstName, lastName) = await _mediator.Send(new GetUserInfoCommand(userId));
        
        return Ok(new
        {
            Success = true,
            Message = "User information fetched successfully",
            Data = new
            {
                FirstName = firstName,
                LastName = lastName
            }
        });
    }
    
    [HttpGet]
    [Route("user/[action]")]
    public async Task<IActionResult> limit()
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var score = await _mediator.Send(new GetUserLimitCommand(userId));
        
        return Ok(new
        {
            Success = true,
            Message = "User score fetched successfully",
            Data = new
            {
                LimitType = score.LimitType,
                MaxAmount = score.MaxAmount
            }
        });
    }
    
    [HttpGet]
    [Route("purchase/details/{purchaseId}")]
    public async Task<IActionResult> purchaseDetails(Guid purchaseId)
    {
        var purchase = await _mediator.Send(new GetPurchaseByIdCommand(purchaseId));
        
        return Ok(new
        {
            Success = true,
            Message = "Purchase details fetched successfully",
            Data = purchase
        });
    }
    
    [HttpGet]
    [Route("purchase/payments/{purchaseId}")]
    public async Task<IActionResult> purchasePayments(Guid purchaseId)
    {
        var payments = await _mediator.Send(new GetPaymentByPurchaseIdCommand(purchaseId));
        
        return Ok(new
        {
            Success = true,
            Message = "Purchase payments fetched successfully",
            Data = new
            {
                Id = payments.Id,
                ProductName = payments.ProductName,
                CreatedTime = payments.CreatedTime,
                MerchantId = payments.MerchantId,
                Schedule = new
                {
                    PaymentDue = payments.Schedule.CreatedTime,
                    Amount = payments.Schedule.Amount
                }
            }
        });
    }
}