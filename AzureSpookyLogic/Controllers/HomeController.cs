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
        public HttpClient _client;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _client = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SpookyRequest spookyRequest) { 
            spookyRequest.Id = Guid.NewGuid().ToString();
            var jsonContent = JsonConvert.SerializeObject(spookyRequest);
            using(var content = new StringContent(jsonContent, Encoding.UTF8,"application/json")) {
                HttpResponseMessage httpResponseMessage = await _client.PostAsync("", content);
            }
            return View();
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