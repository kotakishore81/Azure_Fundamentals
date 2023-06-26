using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisCache.Data;
using RedisCache.Models;
using System.Diagnostics;

namespace RedisCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _applicationDBContext;
        private readonly IDistributedCache _distributedCache;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext,IDistributedCache distributedCache)
        {
            _logger = logger;
            _applicationDBContext = applicationDbContext;
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
          // _distributedCache.Remove("categoryList");
            List<Category> categories = new List<Category>();
          //  var cachedcategory = _distributedCache.GetString("categoryList");
            //if (!string.IsNullOrEmpty(cachedcategory))
            //{
            //    categories = JsonConvert.DeserializeObject<List<Category>>(cachedcategory);
            //}
          //  else
            //{
 
                DistributedCacheEntryOptions options = new();
                options.SetAbsoluteExpiration(new TimeSpan(0, 0, 30));
                categories = _applicationDBContext.Category.ToList();
              //  _distributedCache.SetString("categoryList", JsonConvert.SerializeObject(categories));
           // }
            return View(categories);
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