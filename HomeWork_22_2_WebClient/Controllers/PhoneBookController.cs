using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeWork_22_2_WebClient.Controllers
{
    public class PhoneBookController : Controller
    {
        private IPhoneBook phoneBook;
        private IAppUser appUser;
        public PhoneBookController(IPhoneBook phoneBook, IAppUser appUser)
        {
            this.phoneBook = phoneBook;
            this.appUser = appUser;
        }

        public IActionResult Index()
        {
            var ph = phoneBook.GetPhoneBook();
            ViewData["logIn"] = appUser.logIn;
            ViewData["UserName"] = appUser.UserName;
            return View(ph);
        }
        public RedirectToActionResult AddRecord()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<RedirectToActionResult> AddRecord(string LastName, string FirstName, string ThreeName, string NumberPhone,
            string Address, string Description)
        {
            PhoneBook phoneBookL = new PhoneBook();
            phoneBookL.FirstName = FirstName;
            phoneBookL.LastName = LastName;
            phoneBookL.ThreeName = ThreeName;
            phoneBookL.NumberPhone = NumberPhone;
            phoneBookL.Address = Address;
            phoneBookL.Description = Description;
            await phoneBook.AddRecord(phoneBookL, appUser);
            return RedirectToAction("Index");
        }

        public RedirectToActionResult DeleteRecord()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<RedirectToActionResult> DeleteRecord(int id)
        {
            await phoneBook.DeleteRecord(id, appUser);
            return RedirectToAction("Index");
        }

        public RedirectToActionResult EditRecord()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<RedirectToActionResult> EditRecord(int id, string LastName, string FirstName, string ThreeName, string NumberPhone,
            string Address, string Description)
        {
            PhoneBook phoneBookL = new PhoneBook();
            phoneBookL.PhoneBookID = id;
            phoneBookL.FirstName = FirstName;
            phoneBookL.LastName = LastName;
            phoneBookL.ThreeName = ThreeName;
            phoneBookL.NumberPhone = NumberPhone;
            phoneBookL.Address = Address;
            phoneBookL.Description = Description;
            await phoneBook.EditRecord(phoneBookL, appUser);
            return RedirectToAction("Index");
        }
    }
}