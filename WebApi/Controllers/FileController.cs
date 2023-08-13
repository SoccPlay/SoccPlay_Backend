using Application.Model.Request.RequestFile;
using Application.Model.Respone.ResponseFile;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;

    public FileController(FileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("add")]
    public async Task<ActionResult<ResponseFile>>
        Add([FromForm] RequestFile request) //[FromForm] is request type multipart
    {
        string result = null;
        // var model = _mapper.CarBrandRequestToCar(request);
        if (request.ImageLogo != null)
        {
            result = await _fileService.UploadFile(request.ImageLogo);
        }
        
        return result == null ? BadRequest() : Ok(new
        {
            Success = true,
            Data = result
        });
    }
}