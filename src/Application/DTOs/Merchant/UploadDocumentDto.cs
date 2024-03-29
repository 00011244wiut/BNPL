using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Merchant;

// Data transfer object (DTO) representing a request to upload a document for a merchant.
public record UploadDocumentDto
{
    // The document to upload.
    public IFormFile? Document { get; set; }
}