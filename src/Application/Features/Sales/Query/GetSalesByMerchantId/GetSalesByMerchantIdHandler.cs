using Application.Contracts;
using Application.DTOs.Sales;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Sales.Query.GetSalesByMerchantId;

public class GetSalesByMerchantIdHandler : IRequestHandler<GetSalesByMerchantIdCommand, List<SalesResponseDto>?>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetSalesByMerchantIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<SalesResponseDto>?> Handle(GetSalesByMerchantIdCommand request, CancellationToken cancellationToken)
    {
        var sales = await _unitOfWork.SalesRepository.GetSalesByMerchantId(request.MerchantId);
        
        var salesList = new List<SalesResponseDto>();
        if (sales == null) return salesList;
        foreach (var sale in sales)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(sale.ProductId);
            if (product == null) throw new NotFoundException("Product not found");
            salesList.Add(new SalesResponseDto
            (
                sale.Id,
                sale.ProductId,
                product.ProductName,
                product.PriceAmount,
                sale.CreatedTime
            ));
        }

        return salesList;
    }
}