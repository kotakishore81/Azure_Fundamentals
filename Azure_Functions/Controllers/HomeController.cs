using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure_Functions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Azure_Functions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static readonly HttpClient client = new HttpClient();
        private readonly BlobServiceClient _blobClient;
 
        public HomeController(ILogger<HomeController> logger, BlobServiceClient blobClient)
        {
            _logger = logger;
            _blobClient = blobClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SalesRequest salesRequest, IFormFile file)
        {
            salesRequest.Id = Guid.NewGuid().ToString();

            using (var content = new StringContent(JsonConvert.SerializeObject(salesRequest),
                System.Text.Encoding.UTF8, "application/json"))
            {
                //call our function and pass the content

                HttpResponseMessage response = await client.PostAsync("http://localhost:7071/api/OnSalesUploadedQueue", content);
                string returnValue = response.Content.ReadAsStringAsync().Result;

            }
            if (file != null) {
                var fileName = salesRequest.Id + Path.GetExtension(file.FileName);
                BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient("functionsalesrep");
                var blobclient = blobContainerClient.GetBlobClient(fileName);
                var httpheader = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                };
                await blobclient.UploadAsync(file.OpenReadStream(), httpheader);
                return View();
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
