using Application.Contracts;
using Application.DTOs.Product;
using Application.DTOs.Schedule;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder;
// CreateOrderHandler class to handle creating an order
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, (Guid, ProductResponseDto, ScheduleResponseDto)>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    // Constructor for CreateOrderHandler
    public CreateOrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    // Method to handle creating an order
    public async Task<(Guid, ProductResponseDto, ScheduleResponseDto)> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Validate the request using CreateOrderValidator
        var validationResult = await new CreateOrderValidator().ValidateAsync(request, cancellationToken);
        
        // If validation fails, throw ValidationException
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Retrieve the user by ID from repository
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        
        // Retrieve the product by ID from repository
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(request.ProductId);
        
        // If product is not found, throw NotFoundException
        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        // Create a new purchase entity
        var purchase = new PurchaseEntity()
        {
            CardId = user!.CardId ?? throw new NotFoundException("User did not complete their profile"),
            ProductId = product.Id,
            UserId = user.Id,
            TotalAmount = product.PriceAmount,
            CreatedTime = DateTime.UtcNow

        };
        
        // Add the purchase to the repository
        purchase = await _unitOfWork.PurchaseRepository.AddAsync(purchase);

        // Create a new schedule entity
        var schedule = new SchedulesEntity()
        {
            CardId = user.CardId ?? throw new NotFoundException("User did not complete their profile"),
            PurchaseId = purchase.Id,
            Amount = purchase.TotalAmount,
            PaymentDate = DateTime.UtcNow.AddDays(1),
        };
        
        // Add the schedule to the repository
        schedule = await _unitOfWork.SchedulesRepository.AddAsync(schedule);
        
        // Return the purchase ID, mapped product response DTO, and mapped schedule response DTO
        return (purchase.Id, _mapper.Map<ProductResponseDto>(product), _mapper.Map<ScheduleResponseDto>(schedule));
    }
}
