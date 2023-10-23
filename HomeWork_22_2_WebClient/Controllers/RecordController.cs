using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeWork_22_2_WebClient.Models;

namespace HomeWork_22_2_WebClient.Controllers
{
    public class RecordController : Controller
    {
        private IPhoneBook repository;
        private IAppUser appUser;

        public RecordController(IPhoneBook repo, IAppUser appUser)
        {
            repository = repo;
            this.appUser = appUser;
        }
        public ViewResult Index(PhoneBook phoneBook)
        {
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return View(phoneBook);
        }

        public RedirectToActionResult ViewRecord(int id)
        {
            var phoneBook = repository.GetPhoneBookId(id);
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return RedirectToAction("Index", phoneBook);
        }

        public ViewResult ViewAddRecord()
        {
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return View();
        }
        public ViewResult ViewEditRecord(int id, string LastName, string FirstName, string ThreeName, string NumberPhone, string Address, string Description)
        {
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            PhoneBook phoneBookL = new PhoneBook();
            phoneBookL.PhoneBookID = id;
            phoneBookL.FirstName = FirstName;
            phoneBookL.LastName = LastName;
            phoneBookL.ThreeName = ThreeName;
            phoneBookL.NumberPhone = NumberPhone;
            phoneBookL.Address = Address;
            phoneBookL.Description = Description;
            return View(phoneBookL);
        }
    }
}
