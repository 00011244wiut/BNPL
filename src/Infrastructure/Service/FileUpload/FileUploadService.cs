// Importing necessary namespaces and contracts
using Application.Contracts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

// Namespace for FileUpload service implementation
namespace Infrastructure.Service.FileUpload;


// Implementation of the FileUploadService contract
public class FileUploadService : IFileUploadService
{
    // Field to store Cloudinary instance
    private readonly Cloudinary _cloudinary;

    // Constructor to initialize FileUploadService with Cloudinary settings
    public FileUploadService(IOptions<CloudinarySettings> settings)
    {
        var cloudinarySettings = settings.Value;
        _cloudinary = new Cloudinary(cloudinarySettings.CloudinaryUrl);
    }

    // Method to upload an image asynchronously
    public async Task<string> UploadImageAsync(IFormFile file, string folderName)
    {
        // Allowed image file extensions
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        // Validating file type against allowed extensions
        ValidateFileType(file, allowedExtensions);
        // Validating file size
        ValidateFileSize(file, 2097152); // 2 MB
        // Checking if file is empty
        IsFileEmpty(file);
        
        // Upload parameters for image
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = folderName,
        };
        
        try
        {
            // Uploading image to Cloudinary
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
        catch (Exception e)
        {
            // Handling upload exception
            throw new Exception(e.Message);
        }
    }
    
    // Method to upload a file asynchronously
    public Task<string> UploadFileAsync(IFormFile file, string folderName)
    {
        // Allowed file extensions
        var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
        // Validating file type against allowed extensions
        ValidateFileType(file, allowedExtensions);
        // Validating file size
        ValidateFileSize(file, 2097152); // 2 MB
        // Checking if file is empty
        IsFileEmpty(file);
        
        // Upload parameters for file
        var uploadParams = new RawUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = folderName,
        };
        
        try
        {
            // Uploading file to Cloudinary
            var uploadResult = _cloudinary.Upload(uploadParams);
            return Task.FromResult(uploadResult.SecureUrl.ToString());
        }
        catch (Exception e)
        {
            // Handling upload exception
            throw new Exception(e.Message);
        }
    }

    // Method to validate file size
    private static void ValidateFileSize(IFormFile file, int fileSizeLimit)
    {
        if (file.Length > fileSizeLimit)
        {
            throw new ValidationException("File size must not exceed 2 MB.");
        }
    }
    
    // Method to validate file type against allowed extensions
    private static void ValidateFileType(IFormFile file, string[] allowedExtensions)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(fileExtension))
        {
            throw new ValidationException("Invalid file extension.");
        }
    }
    
    // Method to check if file is empty
    private static void IsFileEmpty(IFormFile file)
    {
        if (file is not { Length: > 0 })
        {
            throw new ValidationException("Please select a file.");
        }
    }
}
