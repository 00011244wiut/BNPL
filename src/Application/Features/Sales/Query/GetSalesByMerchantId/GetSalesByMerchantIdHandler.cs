using Application.Contracts;
using Application.DTOs.Sales;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Sales.Query.GetSalesByMerchantId;
// GetSalesByMerchantIdHandler class to handle getting sales by merchant ID
public class GetSalesByMerchantIdHandler : IRequestHandler<GetSalesByMerchantIdCommand, List<SalesResponseDto>?>
{
    private readonly IUnitOfWork _unitOfWork;
    
    // Constructor for GetSalesByMerchantIdHandler
    public GetSalesByMerchantIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // Method to handle getting sales by merchant ID
    public async Task<List<SalesResponseDto>?> Handle(GetSalesByMerchantIdCommand request, CancellationToken cancellationToken)
    {
        // Retrieve sales by merchant ID from repository
        var sales = await _unitOfWork.SalesRepository.GetSalesByMerchantId(request.MerchantId);
        
        // Initialize a list to store sales response DTOs
        var salesList = new List<SalesResponseDto>();
        
        // If no sales found, return empty list
        if (sales == null) return salesList;
        
        // Iterate through each sale
        foreach (var sale in sales)
        {
            // Retrieve the product by ID from repository
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(sale.ProductId);
            
            // If product is not found, throw NotFoundException
            if (product == null) throw new NotFoundException("Product not found");
            
            // Create a new SalesResponseDto and add it to the list
            salesList.Add(new SalesResponseDto
            (
                sale.Id,
                sale.ProductId,
                product.ProductName,
                product.PriceAmount,
                sale.CreatedTime
            ));
        }

        // Return the list of sales response DTOs
        return salesList;
    }
}