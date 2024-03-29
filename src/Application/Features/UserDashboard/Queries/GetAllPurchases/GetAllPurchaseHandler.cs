using Application.Contracts;  // Importing necessary namespaces
using Application.DTOs.Purchase;  // Importing necessary namespaces
using Application.Exceptions;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetAllPurchases;  // Namespace declaration

public class GetAllPurchaseHandler : IRequestHandler<GetAllPurchaseCommand, List<PurchaseResponseDto>>  // Class declaration
{
    private readonly IUnitOfWork _unitOfWork;  // Field declaration
    
    public GetAllPurchaseHandler(IUnitOfWork unitOfWork)  // Constructor declaration
    {
        _unitOfWork = unitOfWork;  // Assigning constructor parameter to field
    }
    
    public async Task<List<PurchaseResponseDto>> Handle(GetAllPurchaseCommand request, CancellationToken cancellationToken)  // Method declaration
    {
        var purchases = await _unitOfWork.PurchaseRepository.GetPurchaseByUserId(request.UserId);  // Retrieving purchases by user ID
        
        var purchaseResponse = new List<PurchaseResponseDto>();  // Initializing purchase response list

        if (purchases == null) return purchaseResponse;  // Returning empty list if purchases are null
        foreach (var purchase in purchases)  // Looping through purchases
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(purchase.ProductId);  // Retrieving product by ID
            if (product == null) throw new NotFoundException("Product not found");  // Throwing exception if product is null
            purchaseResponse.Add(new PurchaseResponseDto(  // Adding purchase response DTO to list
                purchase.Id,
                product.ProductName,
                purchase.CreatedTime
            ));
        }

        return purchaseResponse;  // Returning purchase response list
    }
}