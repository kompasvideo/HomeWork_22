using Microsoft.AspNetCore.Mvc;
using HomeWork_22_2_WebApi.Models;
using HomeWork_22_2_WebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HomeWork_22_2_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private IPhoneBookRepository repository;
        RoleManager<IdentityRole> roleManager;
        UserManager<IdentityUser> userManager;

        public PhoneBookController(IPhoneBookRepository repo, RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> usrMgr)
        {
            repository = repo;
            roleManager = roleMgr;
            userManager = usrMgr;
        }

        [HttpGet(Name = "GetPhoneBook")]
        public IEnumerable<PhoneBook> Get()
        {
            return repository.PhoneBooks.ToArray();
        }

        [HttpGet("{id}")]
        public PhoneBook GetById(int id)
        {                        
            return repository.PhoneBooks.First(g => g.PhoneBookID == id);
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public void Add([FromBody] PhoneBook value)
        {
            repository.SaveRecord(value);
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        public void Post([FromBody] PhoneBook value)
        {
            repository.SaveRecord(value);
        }

        [Authorize(Roles = "Admins")]
        [HttpPost("{id}")]
        public void Delete(int id)
        {
            repository.DeleteRecord(id);
        }
    }
}
