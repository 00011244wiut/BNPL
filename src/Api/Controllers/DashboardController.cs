using Api.Service;
using Application.DTOs.Product;
using Application.Features.Product.Commands.CreateProduct;
using Application.Features.Product.Commands.UpdateProduct;
using Application.Features.Product.Queries.GetAllProducts;
using Application.Features.Product.Queries.GetProductById;
using Application.Features.Sales.Query.GetSalesByMerchantId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetAllProductsCommand());
        return Ok(new
        {
            Success = true,
            Products = products
        });
    }
    
    [HttpPost]
    [Route("product/[action]")]
    public async Task<IActionResult> create(ProductRequestDto productRequestDto)
    {
        var merchantId = await AuthHelper.GetUserId(User);
        
        var productId = await _mediator.Send(new CreateProductCommand(productRequestDto, merchantId));
        return Ok(new
        {
            Success = true,
            Message = "Product created successfully",
            ProductId = productId
        });
    }
    
    [HttpGet]
    [Route("product/details/{productId}")]
    public async Task<IActionResult> GetProduct(Guid productId)
    {
        var product = await _mediator.Send(new GetProductCommand(productId));
        return Ok(new
        {
            Success = true,
            Product = product
        });
    }
    
    [HttpPut]
    [Route("product/update/{productId}")]
    public async Task<IActionResult> UpdateProduct(Guid productId, ProductUpdateDto productRequestDto)
    {
        var merchantId = await AuthHelper.GetUserId(User);
        
        await _mediator.Send(new UpdateProductCommand(productId, productRequestDto, merchantId));
        return Ok(new
        {
            Success = true,
            Message = "Product updated successfully"
        });
    }
    
    [HttpGet]
    [Route("sales/bymerchant")]
    public async Task<IActionResult> GetSales()
    {
        var merchantId = await AuthHelper.GetUserId(User);
        var sales = await _mediator.Send(new GetSalesByMerchantIdCommand(merchantId));
        return Ok(new
        {
            Success = true,
            Sales = sales
        });
    }
}
