using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using HomeWork_22_2_WebApi.Models;

namespace HomeWork_22_2_WebApi.Controllers
{
    [Authorize(Roles = "Admins")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<IdentityUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> usrMgr)
        {
            roleManager = roleMgr;
            userManager = usrMgr;
        }

        [HttpPost]
        [Route("Role")]
        public IActionResult Post()
        {
            var s = JsonConvert.SerializeObject(roleManager.Roles);
            return Ok(s);
        }

        [HttpPost]
        [Route("GetRole")]
        public async Task<IActionResult> Post([FromBody] string Role)
        {
            List<string> names = new();
            IdentityRole role = await roleManager.FindByIdAsync(Role);
            if (role != null)
            {
                foreach (var user in userManager.Users)
                {
                    if (user != null
                        && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        names.Add(user.UserName);
                    }
                }
            }
            var s = JsonConvert.SerializeObject(names);
            return Ok(s);
        }

        [HttpPost]
        [Route("GetEditRole")]
        public async Task<IActionResult> GetEditRole([FromBody] string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<IdentityUser> members = new();
            List<IdentityUser> nonMembers = new();
            foreach (IdentityUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name)
                    ? members : nonMembers;
                list.Add(user);
            }
            RoleEditModel roleEditModel = new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            var s = JsonConvert.SerializeObject(roleEditModel);
            return Ok(s);
        }

        [HttpPost]
        [Route("SetEditRole")]
        public async Task<IActionResult> Post([FromBody] RoleModificationModel model)
        {
            bool resultB = true;
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    if (userId == "0") continue;
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded)
                        {
                            resultB = false;
                        }
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    if (userId == "0") continue;
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded)
                        {
                            resultB = false;
                        }
                    }
                }
            }
            var s = JsonConvert.SerializeObject(resultB);
            return Ok(s);
        }

        [HttpPost]
        [Route("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] string id)
        {
            bool resultB = true;
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    resultB = false;
                }
            }
            var s = JsonConvert.SerializeObject(resultB);
            return Ok(s);
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] string name)
        {
            bool resultB = true;
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (!result.Succeeded)
                {
                    resultB = false;
                }
            }
            var s = JsonConvert.SerializeObject(resultB);
            return Ok(s);
        }
    }
}
