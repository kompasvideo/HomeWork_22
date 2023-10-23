using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

using HomeWork_22_2_WebApi.Models;
using HomeWork_22_2_WebApi.Interfaces;
using HomeWork_22_2_WebApi.Configuration;
using Microsoft.Extensions.Options;
using System.Linq;

namespace HomeWork_22_2_WebApi.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(IOptions<JwtBearerTokenSettings> jwtTokenOptions,
                UserManager<IdentityUser> userMgr,
                SignInManager<IdentityUser> signinMgr)
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            userManager = userMgr;
            signInManager = signinMgr;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> SignInAndGetToken([FromBody] LoginViewModel loginViewModel)        
        {
            if (! ModelState.IsValid) return BadRequest(ModelState);
            else
            {
                var user = await userManager.FindByNameAsync(loginViewModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(
                                user, loginViewModel.Password, false, false);
                    if (result == Microsoft.AspNetCore.Identity.SignInResult.Failed) return Unauthorized();

                    var token = await GenerateToken(user);
                    return Ok(new { Token = token, Message = "Success" });
                }
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LogOut")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        private async Task<object> GenerateToken(IdentityUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            var roles = await userManager.GetRolesAsync(identityUser);
            string role = roles.ToList().First();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    //new Claim(ClaimTypes.Email, identityUser.Email)
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                }, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType),

                Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
