using AzureBlobStorage.Models;

namespace AzureBlobStorage.Services
{
    public interface IBlobService
    {
        Task<List<string>> GetAllBlobs(string containerName);
        Task<List<Blob>> GetAllBlobsWithUri(string containerName);
        Task<string> GetBlob(string name, string containerName);
        Task<bool> UploadBlob(string name, IFormFile file, string containerName,Blob blob);
        Task DeleteBlob(string name, string containerName);
    }
}
