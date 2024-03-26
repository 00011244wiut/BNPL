using Microsoft.AspNetCore.Http;

namespace Application.DTOs.User;

public record UploadDocumentDto()
{
    public IFormFile? PictureFile { get; set; }
    public int DocumentType { get; set; }
}
// (IFormFile PictureFile, int DocumentType);