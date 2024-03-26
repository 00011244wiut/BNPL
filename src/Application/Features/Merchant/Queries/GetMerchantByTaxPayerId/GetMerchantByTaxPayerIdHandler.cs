using Application.Contracts;
using Application.DTOs.Auth;
using Application.DTOs.LegalData;
using Application.Exceptions;
using Application.Features.Merchant.Queries.GetMerchantById;
using AutoMapper;
using MediatR;

namespace Application.Features.Merchant.Queries.GetMerchantByTaxPayerId;

public class GetMerchantByTaxPayerIdHandler : IRequestHandler<GetMerchantByTaxPayerIdCommand, LegalDataResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetMerchantByTaxPayerIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<LegalDataResponseDto> Handle(GetMerchantByTaxPayerIdCommand request, CancellationToken cancellationToken)
    {
        var merchant = await _unitOfWork.MerchantRepository.GetByTaxPayerIdAsync(request.Id);
        if (merchant == null)
            throw new NotFoundException(
                $"Merchant with id {request.Id} is not found");
        
        var legalId = merchant.LegalDataId ?? throw new NotFoundException("LegalDataId does not exist");
        var legalData = await _unitOfWork.LegalDataRepository.GetByIdAsync(legalId);
        
        return _mapper.Map<LegalDataResponseDto>(legalData);
    }
}