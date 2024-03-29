using Microsoft.AspNetCore.Http;

namespace Application.DTOs.User;

// Data transfer object (DTO) representing a request to upload pictures for a user.
public record UploadPicturesDto
{
    // The selfie image file.
    public IFormFile? Selfie { get; set; }

    // The ID photo image file.
    public IFormFile? IdPhoto { get; set; }
}