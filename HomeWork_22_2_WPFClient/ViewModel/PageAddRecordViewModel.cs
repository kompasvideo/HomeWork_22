using DevExpress.Mvvm;
using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Models;
using HomeWork_22_2_WPFClient.Pages;
using HomeWork_22_2_WPFClient.Services;
using System.Windows.Input;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class PageAddRecordViewModel : ViewModelBase
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static IPhoneBook phoneBook;
        private static IAppUser appUser;
        public static PhoneBook PhoneBook1 { get; set; }
        // Фамилию
        public string LastName { get; set; }
        // Имя
        public string FirstName { get; set; }
        // Отчество
        public string ThreeName { get; set; }
        // Номер телефона
        public string NumberPhone { get; set; }
        //Адрес
        public string Address { get; set; }
        // Описание
        public string Description { get; set; }


        public PageAddRecordViewModel(PageService p_pageService, MessageBus p_messageBus, IPhoneBook p_phoneBook, IAppUser p_appUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            appUser = p_appUser;
            phoneBook = p_phoneBook;
        }
        public PageAddRecordViewModel()
        {
        }

        public ICommand ButtonReturnClickCommand
        {
            get
            {
                return new DelegateCommand(() => { pageService.ChangePage(new Page1AndLoginUser()); });
            }
        }

        public ICommand ButtonAddRecordClickCommand
        {
            get
            {
                var a = new DelegateCommand(async () =>
                {
                    PhoneBook1 = new PhoneBook
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        ThreeName = ThreeName,
                        NumberPhone = NumberPhone,
                        Address = Address,
                        Description = Description
                    };
                    await phoneBook.AddRecord(PhoneBook1, appUser);
                    pageService.ChangePage(new Page1AndLoginUser());
                });
                return a;
            }
        }
    }
}