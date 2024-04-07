using Application.Contracts;
using Application.Exceptions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Commands.ConfirmOrder;
// ConfirmOrderHandler class to handle confirming an order
public class ConfirmOrderHandler : IRequestHandler<ConfirmOrderCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    
    // Constructor for ConfirmOrderHandler
    public ConfirmOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // Method to handle confirming an order
    public async Task<Unit> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        // Validate the request using ConfirmOrderValidator
        var validationResult = await new ConfirmOrderValidator().ValidateAsync(request, cancellationToken);
        
        // If validation fails, throw ValidationException
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        // Retrieve the user by ID from repository
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        
        // If user is not found, throw NotFoundException
        if (user == null)
            throw new NotFoundException("User not found");
        
        // Retrieve the purchase by ID from repository
        var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(request.PurchaseId);
        
        // If purchase is not found, throw NotFoundException
        if (purchase == null)
        {
            throw new NotFoundException("Purchase not found");
        }
        
        // Check if the user ID and card ID in the request match with the purchase
        if (purchase.UserId != request.UserId || purchase.CardId != request.CardId)
        {
            throw new BadRequestException("Invalid purchase");
        }
        
        // Retrieve the schedule by purchase ID from repository
        var schedule = await _unitOfWork.SchedulesRepository.GetScheduleByPurchaseId(request.PurchaseId);

        // Create a new payment entity
        var payment = new PaymentsEntity()
        {
            Amount = purchase.TotalAmount,
            PurchaseId = purchase.Id,
            ScheduleId = (schedule ?? throw new NotFoundException("Schedule not found"))[0].Id,
            PaymentDate = schedule[0].PaymentDate
        };
        
        // Add the payment to the repository
        await _unitOfWork.PaymentsRepository.AddAsync(payment);

        // Retrieve the product by ID from repository
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(purchase.ProductId);

        // Create a new sales entity
        var sales = new SalesEntity()
        {
            CreatedTime = DateTime.UtcNow,
            MerchantId = (product ?? throw new NotFoundException("Product not found")).MerchantId,
            ProductId = purchase.ProductId
        };
        
        // Add the sales to the repository
        await _unitOfWork.SalesRepository.AddAsync(sales);

        return Unit.Value;
    }
}
