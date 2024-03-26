using Application.Contracts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Service.FileUpload;

public class FileUploadService : IFileUploadService
{
    private readonly Cloudinary _cloudinary;

    public FileUploadService(IOptions<CloudinarySettings> settings)
    {
        var cloudinarySettings = settings.Value;
        _cloudinary = new Cloudinary(cloudinarySettings.CloudinaryUrl);
    }

    public async Task<string> UploadImageAsync(IFormFile file, string folderName)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        ValidateFileType(file, allowedExtensions);
        ValidateFileSize(file, 2097152); // 2 MB
        IsFileEmpty(file);
        
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = folderName,
        };
        
        try
        {
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private static void ValidateFileSize(IFormFile file, int fileSizeLimit)
    {
        if (file.Length > fileSizeLimit)
        {
            throw new ValidationException("File size must not exceed 2 MB.");
        }
    }
    
    private static void ValidateFileType(IFormFile file, string[] allowedExtensions)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(fileExtension))
        {
            throw new ValidationException("Invalid file extension.");
        }
    }
    
    private static void IsFileEmpty(IFormFile file)
    {
        if (file is not { Length: > 0 })
        {
            throw new ValidationException("Please select a file.");
        }
    }
}