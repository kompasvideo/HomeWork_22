using DevExpress.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeWork_22_2_WPFClient;
using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Models;
using HomeWork_22_2_WPFClient.Services;
using HomeWork_22_2_WPFClient.Pages;
using HomeWork_22_2_WPFClient.Messages;
using System.Windows.Controls;
using HomeWork_22_2_WPFClient.Model;
using System.Windows;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class Page1ViewModel : ViewModelBase
    {
        private static PageService pageService;
        private static EventBus eventBus;
        private static MessageBus messageBus;
        private static IPhoneBook phoneBook;
        private static IAppUser appUser;
        public static ObservableCollection<PhoneBook> PhoneBooks { get; set; }
        static PhoneBook phoneBook1 { get; set; }

        public Page1ViewModel(PageService p_pageService, EventBus p_eventBus, MessageBus p_messageBus,
            IPhoneBook p_phoneBook, IAppUser p_appUser)
        {
            pageService = p_pageService;
            eventBus = p_eventBus;
            messageBus = p_messageBus;
            phoneBook = p_phoneBook;
            appUser = p_appUser;
            PhoneBooks = new ObservableCollection<PhoneBook>(phoneBook.GetPhoneBook());
        }
        public Page1ViewModel()
        {
        }

        /// <summary>
        /// Нажата кнопка - Создать
        /// </summary>
        public ICommand AddRecord
        {
            get
            {
                
                return new DelegateCommand(async (obj) => 
                {
                    await messageBus.SendTo<PageNotAccessViewModel>(new ReturnPageMessage(new Page1()));
                    pageService.ChangePage(new PageNotAccess()); 
                });
            }
        }

        /// <summary>
        /// Нажата кнопка - Войти
        /// </summary>
        public ICommand Login
        {
            get
            {
                return new DelegateCommand(async (obj) => 
                {
                    await messageBus.SendTo<PageLoginViewModel>(new ReturnPageMessage(new Page1()));
                    pageService.ChangePage(new PageLogin()); 
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
                    var r = PhoneBooks.Where(g => g.PhoneBookID == Int32.Parse(obj.ToString()));
                    var fb = r.FirstOrDefault();
                    await messageBus.SendTo<PageViewRecordViewModel>(new PhoneBookMessages(fb));
                    await messageBus.SendTo<PageViewRecordViewModel>(new ReturnPageMessage(new Page1()));
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
                return new DelegateCommand(async (obj) => 
                {
                    await messageBus.SendTo<PageNotAccessViewModel>(new ReturnPageMessage(new Page1()));
                    pageService.ChangePage(new PageNotAccess()); 
                });
            }
        }

        /// <summary>
        /// Нажата кнопка - Редактировать
        /// </summary>
        public ICommand ButtonEditClickCommand
        {
            get
            {
                return new DelegateCommand(async (obj) => 
                {
                    await messageBus.SendTo<PageNotAccessViewModel>(new ReturnPageMessage(new Page1()));
                    pageService.ChangePage(new PageNotAccess()); 
                });
            }
        }
    }
}