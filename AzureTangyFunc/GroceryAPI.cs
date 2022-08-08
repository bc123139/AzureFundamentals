using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureTangyFunc.Models;
using AzureTangyFunc.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AzureTangyFunc
{
    public class GroceryAPI
    {
        private readonly AzureTangyDbContext _db;

        public GroceryAPI(AzureTangyDbContext db)
        {
            _db = db;
        }

        [FunctionName("CreateGrocery")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "post", Route = "GroceryList")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Creating Grocery List Item.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            GroceryItem_Upsert data = JsonConvert.DeserializeObject<GroceryItem_Upsert>(requestBody);
            var groceryItem = new GroceryItem
            {
                Name = data.Name
            };

            _db.GroceryItems.Add(groceryItem);
            _db.SaveChanges();


            return new OkObjectResult(groceryItem);
        }

        [FunctionName("GetGrocery")]
        public async Task<IActionResult> GetGrocery(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GroceryList")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting Grocery List Items.");
            return new OkObjectResult(await _db.GroceryItems.ToListAsync());
        }

        [FunctionName("GetGroceryById")]
        public async Task<IActionResult> GetGroceryById(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GroceryList/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            log.LogInformation("Getting Grocery List Items.");
            var item = await _db.GroceryItems.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return new NotFoundResult();
            return new OkObjectResult(item);
        }
    }
}
