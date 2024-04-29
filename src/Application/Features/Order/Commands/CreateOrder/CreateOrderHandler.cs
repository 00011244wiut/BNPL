using Application.Contracts;
using Application.DTOs.Product;
using Application.DTOs.Schedule;
using Application.Exceptions;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder;
// CreateOrderHandler class to handle creating an order
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, (bool, Guid, ProductResponseDto, List<ScheduleResponseDto>)>
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
    public async Task<(bool, Guid, ProductResponseDto, List<ScheduleResponseDto>)> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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
            throw new NotFoundException("Product not found");
        
        // Create a list of schedule entities
        var schedules = new List<ScheduleResponseDto>();
        
        // If User State is not CompleteProfile, throw NotFoundException
        if (user == null || user!.UserState != UserState.CompleteProfile)
        {
            // Create 4 schedules for the purchase
            for (var i = 0; i < 4; i++)
            {
                var schedule = new SchedulesEntity()
                {
                    CardId = new Guid(),
                    PurchaseId = new Guid(),
                    Amount = product.PriceAmount / 4,
                    PaymentDate = DateTime.UtcNow.AddDays(i * 30),
                    State = i == 0 ? ScheduleState.Payed : ScheduleState.Scheduled
                };
                
                // Add the schedule to the list
                schedules.Add(_mapper.Map<ScheduleResponseDto>(schedule));
            }
            return (false, new Guid(), _mapper.Map<ProductResponseDto>(product), schedules);
        }

        if (user.CardId is null)
        {
            throw new NotFoundException("User did not complete their profile");
        }

        // Create a new purchase entity
        var purchase = new PurchaseEntity
        {
            CardId = Guid.Empty,
            ProductId = product.Id,
            UserId = Guid.Empty,
            TotalAmount = product.PriceAmount,
            CreatedTime = DateTime.UtcNow
        };
        
        // Add the purchase to the repository
        purchase = await _unitOfWork.PurchaseRepository.AddAsync(purchase);
        
        
        // Create 4 schedules for the purchase
        for (var i = 0; i < 4; i++)
        {
            var schedule = new SchedulesEntity
            {
                CardId = user.CardId.Value,
                PurchaseId = purchase.Id,
                Amount = purchase.TotalAmount / 4,
                PaymentDate = DateTime.UtcNow.AddDays(i * 30),
                State = i == 0 ? ScheduleState.Payed : ScheduleState.Scheduled
            };
            // Add Schedule to Database
            await _unitOfWork.SchedulesRepository.AddAsync(schedule);
            
            // Add the schedule to the list
            schedules.Add(_mapper.Map<ScheduleResponseDto>(schedule));
        } // Return the purchase ID, mapped product response DTO, and mapped schedule response DTO
        return (true, purchase.Id, _mapper.Map<ProductResponseDto>(product), schedules);
    }
}