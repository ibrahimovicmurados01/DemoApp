using DemoApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DemoApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;


        }
    
        public IActionResult Index()
        {
            SharedUserModel sharedUser = null;
            // Read the cookie value
            string jsonData = Request.Cookies["SessionUserData"];
            if (!string.IsNullOrEmpty(jsonData))
            {
                // Deserialize the JSON data to the object
                sharedUser = JsonConvert.DeserializeObject<SharedUserModel>(jsonData);
            }

            return View(sharedUser);
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