using Application.Model.Request.RequestFile;
using Application.Model.Response.ResponseFile;

namespace Application.Service;

public interface FileService
{
    Task<Stream> Get(string name);

    Task<ResponseFile> UploadFile(RequestFile file);
}