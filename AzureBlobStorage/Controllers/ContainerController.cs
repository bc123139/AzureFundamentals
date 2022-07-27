using AzureBlobStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobStorage.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainerService _containerService;

        public ContainerController(IContainerService containerService)
        {
            _containerService = containerService;
        }
        public async Task<IActionResult> Index()
        {
            var containers = await _containerService.GetAllContainer();
            return View(containers);
        }
    }
}
