using LoginDemo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;


namespace LoginDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public List<UserTable> UserTables = new List<UserTable>
        {
            new UserTable { UserId = 1, UserName = "admin", UserPassword = "123456", UserRole = 1 },
            new UserTable { UserId = 2, UserName = "123", UserPassword = "123", UserRole = 2 },
            new UserTable { UserId = 3, UserName = "user1", UserPassword = "123456", UserRole = 3 },
            new UserTable { UserId = 4, UserName = "user2", UserPassword = "123456", UserRole = 4 },
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Index2()
        {
            string str = "";
            ClaimsPrincipal principal = (ClaimsPrincipal)HttpContext.User;
            if (null != principal)
            {
                foreach (Claim claim in principal.Claims)
                {
                    str += "CLAIM TYPE: " + claim.Type + "; CLAIM VALUE: " + claim.Value + "\r\n";
                    //  "\r\n"�O C#�����洫�C�Ÿ��C�]��������X�¤�r�A�ǲ�HTML�� <br />�S����
                }
            }
            ViewBag.Str = str;
            return View();
        }

        public IActionResult AccessDeny()
        {
            return View();    // �ڵ��s���A�v�������B�b���αK�X�����C
        }

        public IActionResult LoginSession()
        {
            return View();
        }

        [HttpPost, ActionName("LoginSession")]
        [ValidateAntiForgeryToken]   // �קKXSS�BCSRF����
        public ActionResult LoginSession(UserTable _userTable)
        {
            return View();
        }
        public IActionResult LoginCookie()
        {
            return View();
        }

        [HttpPost, ActionName("LoginCookie")]
        [ValidateAntiForgeryToken]   // �קKXSS�BCSRF����
        public ActionResult LoginCookie(UserTable _userTable)
        {
            UserTable? _result = null;

            if (ModelState.IsValid)
            {
                var ListOne = from m in UserTables
                              where m.UserName == _userTable.UserName && m.UserPassword == _userTable.UserPassword
                              select m;
                _result = ListOne.FirstOrDefault();
            }
            if (_result == null)
            {
                ViewBag.ErrorMessage = "�b���P�K�X���~";
                return View();
            }

            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _result.UserName),
                //new Claim("SelfDefine_LastName", _result.UserId.ToString()),
                new Claim(ClaimTypes.Role, _result.UserRole.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(
                    Claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // ���o�γ]�w�O�_���Ӥ��\���s��z���Ҥu�@���q�C

                //ExpiresUtc = DateTime.Now.AddDays(7),
                // ���o�γ]�w���Ҳ��Ҩ�����ɶ��C

                //IsPersistent = true,
                // ���o�γ]�w���Ҥu�@���q�O�_��h�ӭn�D�ӫ��򦳮ġC

                //IssuedUtc = <DateTimeOffset>,
                // ���o�γ]�w���Ҳ��Ҫ�ñ�o�ɶ��C

                //RedirectUri = <string>,
                // ���o�γ]�w�n�ΰ� HTTP ���s�ɦV�^���Ȫ�������|�ε��� URI�C
            };
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index2", "Home");

        }
        public IActionResult LoginJwt()
        {
            return View();
        }

        [HttpPost, ActionName("LoginJwt")]
        [ValidateAntiForgeryToken]   // �קKXSS�BCSRF����
        public ActionResult LoginJwt(UserTable _userTable)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Cookie���ҵn�X

            return RedirectToAction("Index", "Home");
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
