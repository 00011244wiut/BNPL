using Application.Contracts;
using Application.DTOs.LegalData;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Merchant.Queries.GetMerchantByTaxPayerId;
// GetMerchantByTaxPayerIdHandler class to handle retrieving merchant by tax payer ID
public class GetMerchantByTaxPayerIdHandler : IRequestHandler<GetMerchantByTaxPayerIdCommand, LegalDataResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    // Constructor for GetMerchantByTaxPayerIdHandler
    public GetMerchantByTaxPayerIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    // Method to handle retrieving merchant by tax payer ID
    public async Task<LegalDataResponseDto> Handle(GetMerchantByTaxPayerIdCommand request, CancellationToken cancellationToken)
    {
        // Retrieve merchant by tax payer ID from repository
        var merchant = await _unitOfWork.MerchantRepository.GetByTaxPayerIdAsync(request.Id);
        
        // If merchant is not found, throw NotFoundException
        if (merchant == null)
            throw new NotFoundException($"Merchant with id {request.Id} is not found");
        
        // Get legal data ID from merchant, throw NotFoundException if it doesn't exist
        var legalId = merchant.LegalDataId ?? throw new NotFoundException("LegalDataId does not exist");
        
        // Retrieve legal data by ID from repository
        var legalData = await _unitOfWork.LegalDataRepository.GetByIdAsync(legalId);
        
        // Map legal data entity to LegalDataResponseDto
        return _mapper.Map<LegalDataResponseDto>(legalData);
    }
}