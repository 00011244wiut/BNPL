using Application.Contracts;
using Application.DTOs.Product;
using AutoMapper;
using MediatR;

namespace Application.Features.Product.Queries.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, List<ProductResponseDto>>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    
    public GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<ProductResponseDto>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.ProductsRepository.GetAllAsync();
        return _mapper.Map<List<ProductResponseDto>>(products);
    }
}