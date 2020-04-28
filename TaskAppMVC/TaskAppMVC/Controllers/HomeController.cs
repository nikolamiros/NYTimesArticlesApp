using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using TaskAppMVC.Helper;
using TaskAppMVC.Models;
using TaskAppMVC.ViewModels;

namespace TaskAppMVC.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HelperApi _api = new HelperApi();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        public async Task<IActionResult> Index(string searchTerm)
        {
            var atricles = new List<Artical>();

            if (string.IsNullOrWhiteSpace(searchTerm) == false)
            {

                if (ModelState.IsValid)
                {
                    HttpClient client = _api.Initial();

                    var response = await client.GetAsync($"api/Search/SearchTask?query={searchTerm}");
                    var result = response.Content.ReadAsStringAsync().Result;
                    atricles = JsonConvert.DeserializeObject<List<Artical>>(result);

                }
            }
            return View(atricles);
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
