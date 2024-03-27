using Application.Contracts;
using Application.DTOs.Auth;
using Application.DTOs.Card;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Queries.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdCommand, (UserResponseDto, CardResponseDto)>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<(UserResponseDto, CardResponseDto)> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        var userEntity = _mapper.Map<UserResponseDto>(user);
        
        var card = await _unitOfWork.CardsRepository
            .GetByIdAsync(user!.CardId ?? 
                          throw new NotFoundException(
                              "User did not complete their profile"));
        var cardEntity = _mapper.Map<CardResponseDto>(card);
        
        return (userEntity, cardEntity);
    }
}