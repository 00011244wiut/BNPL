using Microsoft.AspNetCore.Http;

namespace Application.Contracts;

// Interface for a file upload service providing methods to upload images and documents.
public interface IFileUploadService
{
    // Asynchronously uploads an image file.
    // Parameters:
    //   file: The IFormFile representing the image file to upload.
    //   folderName: (Optional) The folder name where the image will be stored. Default is "images".
    // Returns:
    //   A Task representing the asynchronous operation. The task result contains a string representing the path or URL of the uploaded image.
    Task<string> UploadImageAsync(IFormFile file, string folderName = "images");

    // Asynchronously uploads a document file.
    // Parameters:
    //   file: The IFormFile representing the document file to upload.
    //   folderName: (Optional) The folder name where the document will be stored. Default is "documents".
    // Returns:
    //   A Task representing the asynchronous operation. The task result contains a string representing the path or URL of the uploaded document.
    Task<string> UploadFileAsync(IFormFile file, string folderName = "documents");
}