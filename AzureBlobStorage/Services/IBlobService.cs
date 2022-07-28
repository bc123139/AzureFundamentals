namespace AzureBlobStorage.Services
{
    public interface IBlobService
    {
        Task<List<string>> GetAllBlobs(string containerName);
        Task<List<string>> GetBlob(string name, string containerName);
        Task UploadBlob(string name, IFormFile file, string containerName);
        Task DeleteContainer(string name, string containerName);
    }
}
