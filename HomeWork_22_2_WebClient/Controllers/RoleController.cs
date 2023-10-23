using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HomeWork_22_2_WebClient.Controllers
{
    public class RoleController : Controller
    {
        IRoleUser roleUser;
        IAppUser appUser;
        string[] idsToAdd;
        string[] idsToDelete;
        public RoleController(IRoleUser roleUser, IAppUser appUser)
        {
            this.roleUser = roleUser;
            this.appUser = appUser;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IdentityRole> roles = null;
            try
            {
                roles = await roleUser.GetRoles(appUser);

            }
            catch (Exception ex)
            {
                // вывод окна ошибки
                return RedirectToAction(nameof(Error));
            }
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return View(roles);
        }

        public async Task<IActionResult> Edit(string id)
        {
            RoleEditModel roleEditModel = await roleUser.GetEditRole(id, appUser);
            if (roleEditModel != null)
            {
                ViewData["logIn"] = appUser.logIn;
                ViewData["UserName"] = appUser.UserName;
                return View(roleEditModel);
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            if (model.IdsToAdd == null)
            {
                idsToAdd = new string[1];
                idsToAdd[0] = "0";
                model.IdsToAdd = idsToAdd;
            }
            if (model.IdsToDelete == null)
            {
                idsToDelete = new string[1];
                idsToDelete[0] = "0";
                model.IdsToDelete = idsToDelete;
            }
            bool result = await roleUser.SetEditRole(model, appUser);
            if (result)
            {
                ViewData["logIn"] = appUser.logIn;
                ViewData["UserName"] = appUser.UserName;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Error));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            bool result = await roleUser.DeleteRole(id, appUser);

            if (result)
            {
                ViewData["logIn"] = appUser.logIn;
                ViewData["UserName"] = appUser.UserName;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Error));
        }

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            bool result = await roleUser.CreateRole(name, appUser);

            if (result)
            {
                ViewData["logIn"] = appUser.logIn;
                ViewData["UserName"] = appUser.UserName;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Error));
        }
        [HttpGet]
        public IActionResult Create()
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
