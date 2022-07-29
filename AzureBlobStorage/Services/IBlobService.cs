﻿namespace AzureBlobStorage.Services
{
    public interface IBlobService
    {
        Task<List<string>> GetAllBlobs(string containerName);
        Task<string> GetBlob(string name, string containerName);
        Task<bool> UploadBlob(string name, IFormFile file, string containerName);
        Task DeleteContainer(string name, string containerName);
    }
}
