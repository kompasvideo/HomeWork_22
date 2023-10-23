using DevExpress.Mvvm;
using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Messages;
using HomeWork_22_2_WPFClient.Models;
using HomeWork_22_2_WPFClient.Pages;
using HomeWork_22_2_WPFClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class Page1AndLoginUserViewModel : ViewModelBase
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static IPhoneBook phoneBook;
        private static IAppUser appUser;

        public static ObservableCollection<PhoneBook> PhoneBooks { get; set; }
        static PhoneBook phoneBook1 { get; set; }
        public static string UserName { get; set; }

        public Page1AndLoginUserViewModel(PageService p_pageService, MessageBus p_messageBus,
            IPhoneBook p_phoneBook, IAppUser p_appUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            phoneBook = p_phoneBook;
            appUser = p_appUser;
            PhoneBooks = new ObservableCollection<PhoneBook>(phoneBook.GetPhoneBook());
            UserName = appUser.UserName;
        }
        public Page1AndLoginUserViewModel()
        {
        }

        /// <summary>
        /// Нажата кнопка - Создать
        /// </summary>
        public ICommand AddRecord
        {
            get
            {
                return new DelegateCommand((obj) => { pageService.ChangePage(new PageAddRecord()); });
            }
        }

        /// <summary>
        /// Нажата кнопка - Выйти
        /// </summary>
        public ICommand Logout
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    appUser.Logout();
                    pageService.ChangePage(new Page1());
                });
            }
        }

        /// <summary>
        /// Нажата кнопка - Id
        /// </summary>
        public ICommand ButtonClickCommand
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    var r = PhoneBooks.Where(g => g.PhoneBookID == (Int32)obj);
                    var fb = r.FirstOrDefault();
                    await messageBus.SendTo<PageViewRecordViewModel>(new PhoneBookMessages(fb));
                    await messageBus.SendTo<PageViewRecordViewModel>(new ReturnPageMessage(new Page1AndLoginUser()));
                    pageService.ChangePage(new PageViewRecord());
                });
            }
        }

        /// <summary>
        /// Нажата кнопка - Удалить
        /// </summary>
        public ICommand ButtonDelClickCommand
        {
            get
            {
                var a = new DelegateCommand(async (obj) =>
                {
                    int id = Int32.Parse(obj.ToString());
                    await phoneBook.DeleteRecord(id, appUser);
                    pageService.ChangePage(new Page1AndLoginUser());
                });
                return a;
            }
        }

        /// <summary>
        /// Нажата кнопка - Редактировать
        /// </summary>
        public ICommand ButtonEditClickCommand
        {
            get
            {
                var a = new DelegateCommand(async (obj) =>
                {
                    int id = Int32.Parse(obj.ToString());
                    var r = PhoneBooks.Where(g => g.PhoneBookID == id);
                    var fb = r.FirstOrDefault();
                    await messageBus.SendTo<PageEditRecordViewModel>(new PhoneBookMessages(fb));
                    pageService.ChangePage(new PageEditRecord());
                });
                return a;
            }
        }
        
        /// <summary>
        /// Панель админа
        /// </summary>
        public ICommand ButtonAdminClickCommand
        {
            get
            {
                var a = new DelegateCommand(async () =>
                {
                    await appUser.LoadUsers();
                    var users = appUser.GetUsers();
                    List<AppUser> res = null;
                    if (users != null)
                    {
                        res = users.ToList();
                    }
                    await messageBus.SendTo<PageAdminViewModel>(new UsersMessage(res));
                    pageService.ChangePage(new PageAdmin());
                });
                return a;
            }
        }
    }
}