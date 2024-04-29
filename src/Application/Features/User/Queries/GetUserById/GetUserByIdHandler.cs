using Application.Contracts;  // Importing necessary namespaces
using Application.DTOs.Auth;  // Importing necessary namespaces
using Application.DTOs.Card;  // Importing necessary namespaces
using Application.Exceptions;  // Importing necessary namespaces
using AutoMapper;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.User.Queries.GetUserById;  // Namespace declaration

public class GetUserByIdHandler : IRequestHandler<GetUserByIdCommand, (UserResponseDto, CardResponseDto)>  // Class declaration
{
    private readonly IUnitOfWork _unitOfWork;  // Field declaration
    private readonly IMapper _mapper;  // Field declaration
    
    public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)  // Constructor declaration
    {
        _unitOfWork = unitOfWork;  // Assigning constructor parameters to fields
        _mapper = mapper;  // Assigning constructor parameters to fields
    }

    public async Task<(UserResponseDto, CardResponseDto)> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)  // Method declaration
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);  // Retrieving user from repository
        
        if (user == null)  // If user is null, throw NotFoundException
            throw new NotFoundException("User not found");
        
        var userEntity = _mapper.Map<UserResponseDto>(user);  // Mapping user to user response DTO
        
        var card = await _unitOfWork.CardsRepository  // Retrieving card from repository
            .GetByIdAsync(user!.CardId ??   // If user is null, throw NotFoundException
                          throw new NotFoundException(
                              "User did not complete their profile"));
        if (card is null)
        {
            throw new NotFoundException("User did not complete their profile");
        }

        var cardEntity = new CardResponseDto(card.Id, card.CardType, "****" + card.CardNumber[^4..]);
        return (userEntity, cardEntity);  // Returning user and card response DTOs
    }
}