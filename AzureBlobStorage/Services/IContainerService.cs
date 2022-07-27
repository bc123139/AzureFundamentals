namespace AzureBlobStorage.Services
{
    public interface IContainerService
    {
        Task<List<string>> GetAllContainersAndBlobs();
        Task<List<string>> GetAllContainer();
        Task CreateContainer();
        Task DeleteContainer();
    }
}
