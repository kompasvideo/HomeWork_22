using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HomeWork_22_2_WebClient.Models;
using System.Threading.Tasks;

namespace HomeWork_22_2_WebClient.Controllers
{
    public class AdminController : Controller
    {
        private IAppUser appUser;
        AppUser user;
        public AdminController(IAppUser appUser)
        {
            this.appUser = appUser;
        }

        public ActionResult Index()
        {
            var task = appUser.LoadUsers();
            task.Wait();
            var v = appUser.GetUsers();
            if (v == null) return RedirectToAction("AccessDenied");
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return View(v);
        }

        public ViewResult Create()
        {
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Create(CreateModel model)
        {
            appUser.CreateUser(model).Wait();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            user = await appUser.GetEditUser(id);
            if (user != null)
            {
                ViewData["logIn"] = appUser.logIn;
                ViewData["UserName"] = appUser.UserName;
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(string Id, string Email,
                string UserName)
        {
            try
            {
                var task = appUser.EditUser(Id, Email, UserName);
                task.Wait();
            }
            catch (Exception ex)
            {
                // вывод окна ошибки
                return RedirectToAction("Error");
            }
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            try
            {
                var task = appUser.DeleteUser(id);
                task.Wait();
            }
            catch (Exception ex)
            {
                // вывод окна ошибки
                return RedirectToAction("Error");
            }
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return View();
        }
        public IActionResult Error()
        {
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return View();
        }
    }
}
