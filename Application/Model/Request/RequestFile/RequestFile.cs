using Microsoft.AspNetCore.Http;

namespace Application.Model.Request.RequestFile;

public class RequestFile
{
    public IFormFile? ImageLogo { get; set; }
}