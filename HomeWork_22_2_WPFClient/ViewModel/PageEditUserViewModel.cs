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
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class PageEditUserViewModel : ViewModelBase
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static IAppUser iAppUser;
        public static AppUser appUser { get; set; }
        public PageEditUserViewModel(PageService p_pageService, MessageBus p_messageBus,
            IAppUser p_appUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            iAppUser = p_appUser;
            if (messageBus != null)
            {
                messageBus.Receive<AppUserMessage>(this, async message => { appUser = message.User; });                
            }
        }
        public PageEditUserViewModel()
        {

        }

        /// <summary>
        /// Нажата кнопка "Вернуться назад"
        /// </summary>
        public ICommand ButtonReturnClickCommand
        {
            get
            {
                return new DelegateCommand(() => { pageService.ChangePage(new PageAdmin()); });
            }
        }

        public ICommand ButtonEditUserClickCommand
        {
            get
            {
                var a = new DelegateCommand(async () =>
                {
                    try
                    { 
                        await iAppUser.EditUser(appUser.Id, appUser.Email, appUser.UserName);
                    }
                    catch (Exception)
                    {
                        pageService.ChangePage(new PageError());
                    }
                    pageService.ChangePage(new PageAdmin());
                });
                return a;
            }
        }
    }
}
