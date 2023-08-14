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

    [HttpPost]
    public async Task<ActionResult<ResponseFile>> Add([FromForm]RequestFile requestFile) //[FromForm] is request type multipart
    {
        var responseFile= await _fileService.UploadFile(requestFile);
        return responseFile == null ? BadRequest() : Ok(new
        {
            Success = true,
            Data = responseFile
        });
    }

    [HttpGet]
    public async Task<ActionResult<ResponseFile>> Get(string name)
    {
        var result = await _fileService.Get(name);
        return Ok(new
        {
            Success = true,
            Data = result
        });
    }
}