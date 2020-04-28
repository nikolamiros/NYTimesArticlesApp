using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskAppMVC.Helper;
using TaskAppMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskAppMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HelperApi _api = new HelperApi();

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _api.Initial();

                var response = await client.PostAsJsonAsync("api/Auth/Register", model);

                var result = response.IsSuccessStatusCode;
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }

            return View();
        }



        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                HttpClient client = _api.Initial();

                var response = await client.PostAsJsonAsync("api/Auth/Login", model);

                if (response.IsSuccessStatusCode)
                {
                    var resultMessage = response.Content.ReadAsStringAsync().Result;
                    var parsedJson = JObject.Parse(resultMessage);
                    var tokenBased = (string)parsedJson["token"];

                    var claims = new[]
                    {
                        new Claim("token", string.Format("Bearer {0}", tokenBased)),
                    };

                    var identity = new ClaimsIdentity(claims, "Cookies");



                    return SignIn(new ClaimsPrincipal(identity), "Cookies");

                }

                return View();
            }
            return View();

        }



    }
}
