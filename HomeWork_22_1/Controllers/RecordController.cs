using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeWork_22.Models;

namespace HomeWork_22.Controllers
{
    public class RecordController : Controller
    {
        private IPhoneBookRepository repository;
        public RecordController(IPhoneBookRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(PhoneBook phoneBook)
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(phoneBook);
        }

        public RedirectToActionResult ViewRecord(int id)
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            PhoneBook phoneBook = repository.PhoneBooks
                .FirstOrDefault(p => p.PhoneBookID == id);
            return RedirectToAction("Index", phoneBook);
        }

        public ViewResult ViewAddRecord()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
        public ViewResult ViewEditRecord(int id)
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            PhoneBook phoneBook = repository.PhoneBooks
                .FirstOrDefault(p => p.PhoneBookID == id);
            return View(phoneBook);
        }
    }
}
