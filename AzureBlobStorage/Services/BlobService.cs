using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobStorage.Models;

namespace AzureBlobStorage.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task DeleteBlob(string name, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(name);
            await blobClient.DeleteIfExistsAsync();
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

        public async Task<List<Blob>> GetAllBlobsWithUri(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobs = blobContainerClient.GetBlobsAsync();
            var blobList = new List<Blob>();
            await foreach (var item in blobs)
            {
                var blobClient=blobContainerClient.GetBlobClient(item.Name);
                Blob individualBlob = new Blob
                {
                    Title = blobClient.Uri.AbsoluteUri,

                };
                BlobProperties blobProperties = await blobClient.GetPropertiesAsync();
                if (blobProperties.Metadata.ContainsKey("title"))
                {
                    individualBlob.Title = blobProperties.Metadata["title"];
                }
                if (blobProperties.Metadata.ContainsKey("comment"))
                {
                    individualBlob.Title = blobProperties.Metadata["comment"];
                }
                blobList.Add(individualBlob);
            }
            return blobList;
        }

        public async Task<string> GetBlob(string name, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(name);
            return blobClient.Uri.AbsoluteUri;
        }


        public async Task<bool> UploadBlob(string name, IFormFile file, string containerName,Blob blob)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(name);
            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };
            IDictionary<string,string> metaData=new Dictionary<string,string>();
            metaData.Add("title", blob.Title);
            metaData.Add("comment",blob.Comment);
            var result = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders,metaData);
            //metaData.Remove("title");
            //await blobClient.SetMetadataAsync(metaData);
            if (result!=null) return true;
            return false;
        }
    }
}
