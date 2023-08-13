using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Application.Service.Implement;

public class FileServiceImplement : FileService
{
    private readonly BlobServiceClient _blobServiceClient;
    
    public FileServiceImplement(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }
    
    public async Task<Stream> Get(string name)
    {
        var containerInstance = _blobServiceClient.GetBlobContainerClient("AzureBlobStorage");
        
        var blobInstance = containerInstance.GetBlobClient(name);

        var downloadContent = await blobInstance.DownloadAsync();
        return downloadContent.Value.Content;
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        //get container instance
        var containerInstance = _blobServiceClient.GetBlobContainerClient("AzureBlobStorage");
        // get file name from request and upload to azure blob storage
        var blobName =  $"{Guid.NewGuid()}{file.FileName}";
        // local file path
        var blobInstance = containerInstance.GetBlobClient(blobName);
        
        // upload file to azure blob storage
        await blobInstance.UploadAsync(file.OpenReadStream());
        
        // storageAccountUrl
        var storageAccountUrl = "https://carunistorage.blob.core.windows.net/carunistorage";
        // get blob url
        var blobUrl = $"{storageAccountUrl}/{blobName}";
        
        return blobUrl;
    }
}