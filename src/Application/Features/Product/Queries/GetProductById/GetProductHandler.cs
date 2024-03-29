using Application.Contracts;
using Application.DTOs.Product;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Product.Queries.GetProductById;
// GetProductHandler class to handle getting product by ID
public class GetProductHandler : IRequestHandler<GetProductCommand, ProductResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    // Constructor for GetProductHandler
    public GetProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    // Method to handle getting product by ID
    public async Task<ProductResponseDto> Handle(GetProductCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the product by ID from repository
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(request.Id);
        
        // If product is not found, throw NotFoundException
        if (product == null)
            throw new NotFoundException(
                $"Product with id {request.Id} is not found");

        // Map the product entity to product response DTO
        var productDto = _mapper.Map<ProductResponseDto>(product);
        return productDto;
    }
}