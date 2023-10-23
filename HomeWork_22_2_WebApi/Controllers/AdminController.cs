using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using HomeWork_22_2_WebApi.Interfaces;
using HomeWork_22_2_WebApi.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HomeWork_22_2_WebApi.Controllers
{
    [Authorize(Roles = "Admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private UserManager<IdentityUser> userManager;
        private IUserValidator<IdentityUser> userValidator;
        private IPasswordValidator<IdentityUser> passwordValidator;
        private IPasswordHasher<IdentityUser> passwordHasher;
        RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<IdentityUser> usrMgr,
                IUserValidator<IdentityUser> userValid,
                IPasswordValidator<IdentityUser> passValid,
                IPasswordHasher<IdentityUser> passwordHash,
                RoleManager<IdentityRole> roleMgr)
        {
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            userManager = usrMgr;
            roleManager = roleMgr;
        }

        [HttpPost]
        [Route("Admin")]
        public IActionResult Post()
        {
            var s = JsonConvert.SerializeObject(userManager.Users);
            return Ok(s);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Admin/CreateUser")]
        public async Task<IActionResult> Post([FromBody] CreateModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var identityUser = new IdentityUser() { UserName = model.Name, Email = model.Email };
            var result = await userManager.CreateAsync(identityUser, model.Password);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok(new { Message = "User Reigstration Successful" });
        }

        [HttpPost]
        [Route("Admin/GetEditUser")]
        public async Task<IActionResult> Post([FromBody] string id)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = null;
                try
                {
                    user = await userManager.FindByIdAsync(id);
                }
                catch (Exception ex)
                {
                }
                if (user != null)
                {
                    var s = JsonConvert.SerializeObject(user);
                    return Ok(s);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Admin/EditUser")]
        public async Task<IActionResult> Post([FromBody] IdentityUser appUser)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = null;
                try
                {
                    user = await userManager.FindByIdAsync(appUser.Id);
                    if (user != null)
                    {
                        user.Email = appUser.Email;
                        user.UserName = appUser.UserName;
                        IdentityResult validUserName
                            = await userValidator.ValidateAsync(userManager, user);
                        if (!validUserName.Succeeded)
                        {
                            return BadRequest();
                        }
                        if (validUserName.Succeeded && appUser.UserName != string.Empty)
                        {
                            IdentityResult result = await userManager.UpdateAsync(user);
                            if (result.Succeeded)
                            {
                                return Ok();
                            }
                            else
                            {
                                return BadRequest();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Admin/DeleteUser")]
        public async Task<IActionResult> Post([FromBody] DeleteModel deleteModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = null;
                try
                {
                    user = await userManager.FindByIdAsync(deleteModel.Id);
                    if (user != null)
                    {
                        IdentityResult result = await userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            return Ok();
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return BadRequest();
        }

        
    }
}
