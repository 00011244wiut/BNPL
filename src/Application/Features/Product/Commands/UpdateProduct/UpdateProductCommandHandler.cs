using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Product.Commands.UpdateProduct;
// UpdateProductCommandHandler class to handle updating a product
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    // Constructor for UpdateProductCommandHandler
    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // Method to handle updating a product
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate the request using UpdateProductCommandValidator
        var validationResult = await new UpdateProductCommandValidator().ValidateAsync(request, cancellationToken);

        // If validation fails, throw ValidationException
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        // Check if Merchant exists
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.MerchantId);
        
        // If merchant is not found, throw ValidationException
        if (merchant == null)
            throw new NotFoundException("Merchant not found");
        

        // Retrieve the product by ID from repository
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(request.ProductId);
        
        // If product is not found, throw ValidationException
        if (product == null)
            throw new ValidationException("Product not found");
        
        // Check if the merchant is Authorized to update the product
        if (merchant!.Id != product.MerchantId)
            throw new ValidationException("Merchant is not authorized to update the product");

        // Map the product DTO to ProductsEntity
        var productEntity = _mapper.Map<ProductsEntity>(product);
        _mapper.Map(request.ProductDto, productEntity);
        
        // Update the product in the repository
        await _unitOfWork.ProductsRepository.UpdateAsync(productEntity);
        
        return Unit.Value;
    }
}