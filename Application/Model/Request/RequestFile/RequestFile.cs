using Microsoft.AspNetCore.Http;

namespace Application.Model.Request.RequestFile;

public class RequestFile
{
    public Guid LandId { get; set; }
    public IFormFile? ImageLogo { get; set; }
}