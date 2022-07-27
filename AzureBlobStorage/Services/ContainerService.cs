using Azure.Storage.Blobs;

namespace AzureBlobStorage.Services
{
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ContainerService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public Task CreateContainer()
        {
            throw new NotImplementedException();
        }

        public Task DeleteContainer()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllContainer()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllContainersAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}
