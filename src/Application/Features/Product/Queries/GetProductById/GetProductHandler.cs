using Application.Contracts;
using Application.DTOs.Auth;
using Application.DTOs.Product;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Product.Queries.GetProductById;

public class GetProductHandler : IRequestHandler<GetProductCommand, ProductResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ProductResponseDto> Handle(GetProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(request.Id);
        if (product == null)
            throw new NotFoundException(
                $"Product with id {request.Id} is not found");

        var productDto = _mapper.Map<ProductResponseDto>(product);
        return productDto;
    }
}