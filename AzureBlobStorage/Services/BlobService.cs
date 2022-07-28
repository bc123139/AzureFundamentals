using Azure.Storage.Blobs;

namespace AzureBlobStorage.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public Task DeleteContainer(string name, string containerName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetAllBlobs(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobs = blobContainerClient.GetBlobsAsync();
            var blobsString=new List<string>();
            await foreach (var blob in blobs)
            {
                blobsString.Add(blob.Name);
            }
            return blobsString;
        }

        public Task<List<string>> GetBlob(string name, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task UploadBlob(string name, IFormFile file, string containerName)
        {
            throw new NotImplementedException();
        }
    }
}
