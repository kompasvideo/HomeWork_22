using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HomeWork_22.Models;
using Microsoft.AspNetCore.Authorization;

namespace HomeWork_22.Controllers
{
    public class PhoneBookController : Controller
    {
        private IPhoneBookRepository repository;

        public PhoneBookController(IPhoneBookRepository repo)
        {
            repository = repo;
        }

        
        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(repository.PhoneBooks);
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        public RedirectToActionResult DeleteRecord(int id)
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            repository.DeleteRecord(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public RedirectToActionResult AddRecord(string LastName, string FirstName, string ThreeName, string NumberPhone,
            string Address, string Description)
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            PhoneBook phoneBook = new PhoneBook();
            phoneBook.FirstName = FirstName;
            phoneBook.LastName = LastName;
            phoneBook.ThreeName = ThreeName;
            phoneBook.NumberPhone = NumberPhone;
            phoneBook.Address = Address;
            phoneBook.Description = Description;
            repository.SaveRecord(phoneBook);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        public RedirectToActionResult EditRecord(int id, string LastName, string FirstName, string ThreeName, string NumberPhone,
            string Address, string Description)
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            PhoneBook phoneBook = new PhoneBook();
            phoneBook.PhoneBookID = id;
            phoneBook.FirstName = FirstName;
            phoneBook.LastName = LastName;
            phoneBook.ThreeName = ThreeName;
            phoneBook.NumberPhone = NumberPhone;
            phoneBook.Address = Address;
            phoneBook.Description = Description;
            repository.SaveRecord(phoneBook);
            return RedirectToAction("Index");
        }
    }
}
