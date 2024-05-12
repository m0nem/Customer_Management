using Customer_Management.MVC.Contracts;
using Customer_Management.MVC.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Customer_Management.MVC.Controllers
{
    public class UsersController : Controller
    {

        IAuthenticateService _authenticateService;

        public UsersController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(register);
                }
                var isCreated = await _authenticateService.Register(register);
                if (isCreated)
                {
                    return LocalRedirect("/");
                }
                ModelState.AddModelError("", $"Registration Failed\n");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"{Utilities.Utility.ExtractErrors(ex.Message)}");
            }

            return View(register);
        }
        #endregion

        #region Login

        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authenticateService.Authenticate(login.Email, login.Password);
            if (isLoggedIn)
            {
                return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("", "Login Failed. Please Try again");
            return View(login);
        }
        #endregion

        #region Logout

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authenticateService.Logout();
            return RedirectToAction("/Users/Login");
        }
        #endregion
    }
}
