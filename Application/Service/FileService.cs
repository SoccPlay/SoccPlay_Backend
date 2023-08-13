using Microsoft.AspNetCore.Http;

namespace Application.Service;

public interface FileService
{
    Task<Stream> Get(string name);

    Task<string> UploadFile(IFormFile file);
}