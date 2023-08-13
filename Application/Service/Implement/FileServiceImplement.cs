using Application.Common;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Application.Service.Implement;

public class FileServiceImplement : FileService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly AppConfiguration _appConfiguration;
    
    public FileServiceImplement(BlobServiceClient blobServiceClient, AppConfiguration appConfiguration)
    {
        _appConfiguration = appConfiguration;
        _blobServiceClient = blobServiceClient;
    }
    
    public async Task<Stream> Get(string name)
    {
        var containerInstance = _blobServiceClient.GetBlobContainerClient(_appConfiguration.ContainerName);
        
        var blobInstance = containerInstance.GetBlobClient(name);

        var downloadContent = await blobInstance.DownloadAsync();
        return downloadContent.Value.Content;
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        //get container instance
        var containerInstance = _blobServiceClient.GetBlobContainerClient(_appConfiguration.ContainerName);
        // get file name from request and upload to azure blob storage
        var blobName =  $"{Guid.NewGuid()}{file.FileName}";
        // local file path
        var blobInstance = containerInstance.GetBlobClient(blobName);
        
        // upload file to azure blob storage
        await blobInstance.UploadAsync(file.OpenReadStream());
        
        // storageAccountUrl
        var storageAccountUrl = "https://storerecruitment.blob.core.windows.net/recruitmentwebapi";
        // get blob url
        var blobUrl = $"{storageAccountUrl}/{blobName}";
        
        return blobUrl;
    }
}