using Application.Contracts;
using Application.DTOs.Auth;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Merchant.Queries.GetMerchantById;
// GetMerchantHandler class to handle retrieving merchant by ID
public class GetMerchantHandler : IRequestHandler<GetMerchantCommand, MerchantResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    // Constructor for GetMerchantHandler
    public GetMerchantHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    // Method to handle retrieving merchant by ID
    public async Task<MerchantResponseDto> Handle(GetMerchantCommand request, CancellationToken cancellationToken)
    {
        // Retrieve merchant by ID from repository
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.Id);
        
        // If merchant is not found, throw NotFoundException
        if (merchant == null)
            throw new NotFoundException($"Merchant with id {request.Id} is not found");

        // Map merchant entity to MerchantResponseDto
        var merchantDto = _mapper.Map<MerchantResponseDto>(merchant);
        return merchantDto;
    }
}