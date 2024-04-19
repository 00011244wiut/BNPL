using Application.Contracts;
using Application.DTOs.Product;
using AutoMapper;
using MediatR;

namespace Application.Features.Product.Queries.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, List<ProductResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<ProductResponseDto>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
    {
        
        var products = await _unitOfWork.ProductsRepository.GetProductByMerchantId(request.MerchantId);
        return _mapper.Map<List<ProductResponseDto>>(products);
    }
}