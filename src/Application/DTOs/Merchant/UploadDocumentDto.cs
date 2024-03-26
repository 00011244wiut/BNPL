using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Merchant;

public record UploadDocumentDto()
{
    public IFormFile? Document { get; set; }
}