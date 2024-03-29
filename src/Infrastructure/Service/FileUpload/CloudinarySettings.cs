// Namespace for FileUpload service implementation
namespace Infrastructure.Service.FileUpload;

// Class to hold Cloudinary settings
public class CloudinarySettings
{
    // Constant to represent the section name in configuration
    public const string SectionName = "CloudinarySettings";
    
    // Property to store Cloudinary URL
    public string CloudinaryUrl { get; set; } = null!;
}