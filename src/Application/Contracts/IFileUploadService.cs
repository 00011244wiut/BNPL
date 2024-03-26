using Microsoft.AspNetCore.Http;

namespace Application.Contracts;

public interface IFileUploadService
{
    Task<string> UploadImageAsync(IFormFile file, string folderName = "images");
}