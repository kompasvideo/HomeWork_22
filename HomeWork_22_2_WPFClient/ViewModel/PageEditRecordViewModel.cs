using DevExpress.Mvvm;
using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Messages;
using HomeWork_22_2_WPFClient.Models;
using HomeWork_22_2_WPFClient.Pages;
using HomeWork_22_2_WPFClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class PageEditRecordViewModel : ViewModelBase
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static IPhoneBook phoneBook;
        private static IAppUser appUser;
        public static PhoneBook PhoneBook1 { get; set; }        

        public PageEditRecordViewModel(PageService p_pageService, MessageBus p_messageBus, IPhoneBook p_phoneBook, IAppUser p_appUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            appUser = p_appUser;
            phoneBook = p_phoneBook;
            if (messageBus != null)
            {
                messageBus.Receive<PhoneBookMessages>(this, async message => { PhoneBook1 = message.phoneBook; });
            }
        }
        public PageEditRecordViewModel()
        {
        }

        public ICommand ButtonReturnClickCommand
        {
            get
            {
                return new DelegateCommand(() => { pageService.ChangePage(new Page1()); });
            }
        }

        public ICommand ButtonEditRecordClickCommand
        {
            get
            {
                var a = new DelegateCommand(async () =>
                {                    
                    await phoneBook.EditRecord(PhoneBook1, appUser);
                    pageService.ChangePage(new Page1AndLoginUser());
                });
                return a;
            }
        }
    }
}