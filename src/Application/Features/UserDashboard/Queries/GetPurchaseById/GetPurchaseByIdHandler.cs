using Application.Contracts;  // Importing necessary namespaces
using Application.DTOs.Purchase;  // Importing necessary namespaces
using Application.Exceptions;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetPurchaseById;  // Namespace declaration

public class GetPurchaseByIdHandler : IRequestHandler<GetPurchaseByIdCommand, PurchaseResponseDto>  // Class declaration
{
    private readonly IUnitOfWork _unitOfWork;  // Field declaration
    
    public GetPurchaseByIdHandler(IUnitOfWork unitOfWork)  // Constructor declaration
    {
        _unitOfWork = unitOfWork;  // Assigning constructor parameter to field
    }
    
    public async Task<PurchaseResponseDto> Handle(GetPurchaseByIdCommand request, CancellationToken cancellationToken)  // Method declaration
    {
        var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(request.PurchaseId);  // Retrieving purchase by ID
        
        if (purchase == null) throw new NotFoundException("Purchase not found");  // Throwing exception if purchase is null
        
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(purchase.ProductId);  // Retrieving product by ID
        if (product == null) throw new NotFoundException("Product not found");  // Throwing exception if product is null
        
        return new PurchaseResponseDto(  // Returning purchase response DTO
            purchase.Id,
            product.ProductName,
            purchase.CreatedTime
        );
    }
}