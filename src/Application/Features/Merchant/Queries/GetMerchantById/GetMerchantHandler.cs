using Application.Contracts;
using Application.DTOs.Auth;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Merchant.Queries.GetMerchantById;

public class GetMerchantHandler : IRequestHandler<GetMerchantCommand, MerchantResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetMerchantHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<MerchantResponseDto> Handle(GetMerchantCommand request, CancellationToken cancellationToken)
    {
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.Id);
        if (merchant == null)
            throw new NotFoundException(
                $"Merchant with id {request.Id} is not found");

        var merchantDto = _mapper.Map<MerchantResponseDto>(merchant);
        return merchantDto;
    }
}