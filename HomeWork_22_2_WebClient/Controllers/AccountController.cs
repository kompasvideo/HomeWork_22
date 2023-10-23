using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HomeWork_22_2_WebClient.Models;

namespace HomeWork_22_2_WebClient.Controllers {

    public class AccountController : Controller 
    {
        private readonly IAppUser appUser;
        public AccountController(IAppUser appUser)
        {
            this.appUser = appUser;
        }

        public IActionResult Login(string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = Url.Action("Index", "PhoneBook");
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel details,
                string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (appUser.Login(details))
                {
                    return RedirectToAction("Index", "PhoneBook");
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(details);
        }

        public IActionResult Logout()
        {
            appUser.Logout();
            return RedirectToAction("Index", "PhoneBook");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return View();
        }
    }
}
