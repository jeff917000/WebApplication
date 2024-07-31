using LoginDemo.Utilities;

namespace LoginDemo.Controllers
{
    public class AccessAccountController : Controller
    {
        private readonly ILogger<AccessAccountController> _logger;

        public AccessAccountController(ILogger<AccessAccountController> logger)
        {
            _logger = logger;
        }

        public List<UserTable> UserTables = new List<UserTable>
        {
            new UserTable { UserId = 1, UserName = "admin", UserPassword = "Uh9Tp5TylBkyhnsr0DzKkA==", UserRole = 1 },
            new UserTable { UserId = 2, UserName = "user0", UserPassword = "Uh9Tp5TylBkyhnsr0DzKkA==", UserRole = 2 },
            new UserTable { UserId = 3, UserName = "user1", UserPassword = "Uh9Tp5TylBkyhnsr0DzKkA==", UserRole = 3 },
            new UserTable { UserId = 4, UserName = "user2", UserPassword = "Uh9Tp5TylBkyhnsr0DzKkA==", UserRole = 4 },
            //TODO:密碼加密
        };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]   // 避免XSS、CSRF攻擊
        public ActionResult Login(UserTable _userTable)
        {
            UserTable? _result = null;

            if (UserTables == null)
            {
                ViewBag.ErrorMessage = "系統錯誤。";
                return View();
            }
            else if (ModelState.IsValid)
            {
                Encryption_Library Aes = new Encryption_Library();
                string AesEncrypt = Aes.AesEncrypt(_userTable.UserPassword);
                _userTable.UserPassword = AesEncrypt;

                var ListOne = from m in UserTables
                              where m.UserName == _userTable.UserName && m.UserPassword == _userTable.UserPassword
                              select m;
                _result = ListOne.FirstOrDefault();
            }

            if (_result == null)
            {
                ViewBag.ErrorMessage = "帳號與密碼有誤。";
                return View();
            }

            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _result.UserName),
                new Claim(ClaimTypes.Role, _result.UserRole.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(
                    Claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // 取得或設定是否應該允許重新整理驗證工作階段。

                //ExpiresUtc = DateTime.Now.AddDays(7),
                // 取得或設定驗證票證到期的時間。

                //IsPersistent = true,
                // 取得或設定驗證工作階段是否跨多個要求而持續有效。

                //IssuedUtc = <DateTimeOffset>,
                // 取得或設定驗證票證的簽發時間。

                //RedirectUri = <string>,
                // 取得或設定要用做 HTTP 重新導向回應值的完整路徑或絕對 URI。
            };
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index2", "Home");
        }


        public IActionResult AccessDeny()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Cookie驗證登出
            HttpContext.Session.Clear();
            // 清除Session

            //return RedirectToAction("Index", "Home");
            return View();
        }
    }
}
