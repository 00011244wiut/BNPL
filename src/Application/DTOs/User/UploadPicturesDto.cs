using Microsoft.AspNetCore.Http;

namespace Application.DTOs.User;

public record UploadPicturesDto()
{
    
    public IFormFile? Selfie { get; set; }
    public IFormFile? IdPhoto { get; set; }
}