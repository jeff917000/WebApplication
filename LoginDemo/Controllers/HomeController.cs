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
        [Authorize/*(Roles = "1,2")*/]
        public IActionResult Index2()
        {
            string str = "";

            ClaimsPrincipal principal = (ClaimsPrincipal)HttpContext.User;
            if (principal != null)
            {
                foreach (Claim claim in principal.Claims)
                {
                    str += "CLAIM TYPE: " + claim.Type + ";<br>" + "CLAIM VALUE: " + claim.Value + "<br>";
                }
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
