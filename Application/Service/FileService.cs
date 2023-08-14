using Application.Model.Request.RequestFile;
using Application.Model.Respone.ResponseFile;
using Microsoft.AspNetCore.Http;

namespace Application.Service;

public interface FileService
{
    Task<Stream> Get(string name);

    Task<ResponseFile> UploadFile(RequestFile file);
}