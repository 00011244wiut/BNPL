using Application.Contracts; // Importing application contracts/interfaces
using Application.DTOs.Auth;
using Application.Exceptions; // Importing authentication related DTOs
using AutoMapper; // Importing AutoMapper for object mapping
using Domain.Constants; // Importing domain constants
using Domain.Entities; // Importing domain entities
using FluentValidation; // Importing FluentValidation for validation
using MediatR; // Importing MediatR for handling commands and queries

namespace Application.Features.User.Commands.SubmitCard; // Namespace declaration for the SubmitCardHandler feature

// Handler for the SubmitCardCommand
public class SubmitCardHandler : IRequestHandler<SubmitCardCommand, UserResponseDto>
{
    private readonly IUnitOfWork _unitOfWork; // Dependency injection for unit of work
    private readonly IMapper _mapper; // Dependency injection for AutoMapper
    
    public SubmitCardHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork; // Assigning injected unit of work
        _mapper = mapper; // Assigning injected AutoMapper
    }
    
    // Handling the SubmitCardCommand
    public async Task<UserResponseDto> Handle(SubmitCardCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new SubmitCardValidator().ValidateAsync(request, cancellationToken); // Validating the command
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors); // Throwing validation exception if command is not valid
        }
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId); // Getting user by Id
        
        if (user == null)
            throw new NotFoundException("User not found"); // Throwing not found exception if user is null
        
        var userEntity = _mapper.Map<UserEntity>(user); // Mapping user DTO to user entity
        
        var card = _mapper.Map<CardsEntity>(request.SubmitCardDto); // Mapping submit card DTO to card entity
        card.CardType = GetCardType(request.SubmitCardDto.CardNumber);
        card = await _unitOfWork.CardsRepository.AddAsync(card); // Adding card to repository
        
        userEntity.CardId = card.Id; // Assigning card Id to user entity
        
        // Checking if user state is verification completed and user has first name and purchase limit Id
        if (userEntity.UserState == UserState.VerificationCompleted &&
            userEntity is { FirstName: not null, PurchaseLimitId: not null })
            userEntity.UserState = UserState.CompleteProfile; // Updating user state
        
        await _unitOfWork.UserRepository.UpdateAsync(userEntity); // Updating user entity in repository
        return _mapper.Map<UserResponseDto>(userEntity); // Mapping user entity to response DTO
    }
    private CardTypes GetCardType(string cardNumber)
    {
        if (cardNumber.StartsWith("98")) return CardTypes.Humo;
        if (cardNumber.StartsWith("86")) return CardTypes.Uzcard;
        if (cardNumber.StartsWith("56")) return CardTypes.MasterCard;
        return cardNumber.StartsWith("44") ? CardTypes.VISA : CardTypes.Uzcard;
    }
}
