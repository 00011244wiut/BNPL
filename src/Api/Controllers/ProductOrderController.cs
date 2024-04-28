using Api.Service;
using Application.Features.Order.Commands.ConfirmOrder;
using Application.Features.Order.Commands.CreateOrder;
using Application.Features.User.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class ProductOrderController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProductOrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("order/[action]")]
    public async Task<IActionResult> create(Guid productId)
    {
        var merchantId = await AuthHelper.GetUserId(User);
        var (complete, purchaseId, product, schedule) =
            await _mediator.Send(new CreateOrderCommand(productId, merchantId));

        return Ok(new
        {
            Success = complete,
            Message = "Order created successfully",
            Data = new
            {
                PurchaseId = purchaseId,
                Product = product,
                Schedule = schedule
            }
        });
    }
    
    [HttpPost]
    [Route("order/[action]")]
    public async Task<IActionResult> create_demo(Guid productId)
    {
        var (complete, purchaseId, product, schedule) =
            await _mediator.Send(new CreateOrderCommand(productId, new Guid()));

        return Ok(new
        {
            Success = complete,
            Message = "Order created successfully",
            Data = new
            {
                PurchaseId = purchaseId,
                Product = product,
                Schedule = schedule
            }
        });
    }
    
    [HttpGet]
    [Route("user/[action]")]
    public async Task<IActionResult> getUserInfo()
    {
        var userId = await AuthHelper.GetUserId(User);
        
        var (user, card) = await _mediator.Send(new GetUserByIdCommand(userId));
        
        return Ok(new
        {
            Success = true,
            Message = "User information fetched successfully",
            Data = new
            {
                UserEntity = user,
                CardEntity = card
            }
        });
    }
    
    [HttpPost]
    [Route("order/[action]")]
    public async Task<IActionResult> confirm(Guid PurchaseId, Guid CardId)
    {
        var userId = await AuthHelper.GetUserId(User);
        var confirmOrderCommand = await _mediator.Send(new ConfirmOrderCommand(userId, PurchaseId, CardId));
    
        return Ok(new
        {
            Success = true,
            Message = "Order confirmed successfully",
            Data = confirmOrderCommand
        });
    }
}