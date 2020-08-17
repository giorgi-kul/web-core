using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCore.AdminUI.Models;
using WebCore.Domain.Entities;
using WebCore.Domain.Interfaces;

namespace WebCore.AdminUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _loginView = "~/Views/Login/Login.cshtml";
        private readonly UserManager<Administrator> _userManager;
        private readonly ILogger<LoginController> _logger;
        private readonly IUserActionService _userActionService;

        public LoginController(
            UserManager<Administrator> userManager,
            ILogger<LoginController> logger,
            IUserActionService userActionService)
        {
            _userManager = userManager;
            _logger = logger;
            _userActionService = userActionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_loginView);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind] LoginModel loginModel)
        {
            try
            {
                Administrator administrator = await _userManager.FindByEmailAsync(loginModel.Email);
                bool isValidPassword = await _userManager.CheckPasswordAsync(administrator, loginModel.Password);

                if (isValidPassword)
                {
                    List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, loginModel.Email) };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                    AuthenticationProperties authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                    await _userActionService.CreateLoginAction($"{nameof(LoginController)}:{nameof(Index)}", administrator);

                    return new RedirectResult(loginModel?.ReturnUrl ?? "/");
                }
                else
                {
                    loginModel.ErrorMessage = "Email and password combination is incorrect";
                    return View(_loginView, loginModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Login critical exception");
            }

            return View();
        }
    }
}
