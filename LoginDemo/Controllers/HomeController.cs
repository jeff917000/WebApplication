using LoginDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;


namespace LoginDemo.Controllers
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
            return View();
        }
        //[Authorize(Roles = "1,2")]
        public IActionResult Index2()
        {
            string str = "";
            ClaimsPrincipal principal = (ClaimsPrincipal)HttpContext.User;
            if (null != principal)
            {
                foreach (Claim claim in principal.Claims)
                {
                    str += "CLAIM TYPE: " + claim.Type + "; CLAIM VALUE: " + claim.Value + "\r\n";
                }
            }
            else
            {
                str = HttpContext.Session.GetString("Role");
            }

            ViewBag.Str = str;
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
