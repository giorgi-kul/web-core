using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace WebCore.AdminUI.Controllers
{
    public class LogoutController : Controller
    {
        private readonly CookieAuthenticationOptions _cookieAuthenticationOptions;

        public LogoutController(IOptionsMonitor<CookieAuthenticationOptions> cookieAuthenticationOptions)
        {
            _cookieAuthenticationOptions = cookieAuthenticationOptions.Get(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return new RedirectResult(_cookieAuthenticationOptions.LoginPath);
        }

    }
}