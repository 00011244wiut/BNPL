using Application.Contracts;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Product.Commands.CreateProduct;
// CreateProductHandler class to handle creating a product
public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    // Constructor for CreateProductHandler
    public CreateProductHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    // Method to handle creating a product
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate the request using CreateProductValidator
        var validationResult = await new CreateProductValidator().ValidateAsync(request, cancellationToken);
        
        // If validation fails, throw ValidationException
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        // Map the product request DTO to ProductsEntity
        var product = _mapper.Map<ProductsEntity>(request.ProductRequestDto);
        product.MerchantId = request.MerchantId;
        
        // Add the product to the repository
        product = await _unitOfWork.ProductsRepository.AddAsync(product);
        
        // Return the ID of the created product
        return product.Id;
    }
}