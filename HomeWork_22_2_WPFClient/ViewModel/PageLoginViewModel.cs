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
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class PageLoginViewModel : ViewModelBase
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        /// <summary>
        /// Логин
        /// </summary>
        public static string LoginName { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public static string Password { get; set; }
        static LoginModel loginModel;
        private static IAppUser appUser;
        private static Page page;

        public PageLoginViewModel(PageService p_pageService, MessageBus p_messageBus, IAppUser p_appUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            appUser = p_appUser;
            if (messageBus != null)
            {
                messageBus.Receive<ReturnPageMessage>(this, async message => { page = message.ReturnPage; });
            }
        }
        public PageLoginViewModel()
        {
        }

        public ICommand ButtonReturnClickCommand
        {
            get
            {
                return new DelegateCommand(() => { pageService.ChangePage(page); });
            }
        }

        public ICommand ButtonLoginClickCommand
        {
            get
            {
                var a = new DelegateCommand(async () =>
                {
                    loginModel = new LoginModel();
                    loginModel.Password = Password;
                    loginModel.Name = LoginName;
                    if (appUser.Login(loginModel))
                    {
                        pageService.ChangePage(new Page1AndLoginUser());
                    }
                    else
                    {
                        await messageBus.SendTo<PageErrorViewModel>(new ReturnPageMessage(page));
                        pageService.ChangePage(new PageError());
                    }
                });
                return a;
            }
        }
    }
}
