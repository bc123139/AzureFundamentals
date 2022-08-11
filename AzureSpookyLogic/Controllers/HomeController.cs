using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureSpookyLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace AzureSpookyLogic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static readonly HttpClient client = new HttpClient();
        private readonly BlobServiceClient _blobServiceClient;
        public HomeController(ILogger<HomeController> logger, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SpookyRequest spookyRequest, IFormFile file)
        {
            spookyRequest.Id = Guid.NewGuid().ToString();
            var jsonContent = JsonConvert.SerializeObject(spookyRequest);
            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage httpResponse = await client
                    .PostAsync("https://prod-04.centralus.logic.azure.com:443/workflows/f30160e50b8f4dd388ff488e42ba487a/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=TsPz1unGU1CBHRf5GjCWA4RKcrWB5Tel1aWCk71wXOA", content);
            }

            if (file != null)
            {
                var fileName = spookyRequest.Id + Path.GetExtension(file.FileName);
                BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient("logicappholder");
                var blobClient = blobContainerClient.GetBlobClient(fileName);

                var httpHeaders = new BlobHttpHeaders()
                {
                    ContentType = file.ContentType
                };
                await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);
            }


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}