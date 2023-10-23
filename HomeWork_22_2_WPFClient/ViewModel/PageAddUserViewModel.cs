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
    public class PageAddUserViewModel : ViewModelBase
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static IAppUser appUser;
        public static string UserName { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }

        public PageAddUserViewModel(PageService p_pageService, MessageBus p_messageBus, IAppUser p_appUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            appUser = p_appUser;
        }
        public PageAddUserViewModel()
        {
        }

        public ICommand ButtonReturnClickCommand
        {
            get
            {
                return new DelegateCommand(() => { pageService.ChangePage(new PageAdmin()); });
            }
        }
        public ICommand ButtonAddUserClickCommand
        {
            get
            {
                var a = new DelegateCommand(async () =>
                {
                    CreateModel createModel = new CreateModel
                    {
                        Name = UserName,
                        Email = Email,
                        Password = Password
                    };
                    if (await appUser.CreateUser(createModel))
                    {
                        await appUser.LoadUsers();
                        var res = appUser.GetUsers().ToList();
                        await messageBus.SendTo<PageAdminViewModel>(new UsersMessage(res));
                        pageService.ChangePage(new PageAdmin());
                    }
                    else
                    {
                        pageService.ChangePage(new PageError());
                    }
                });
                return a;
            }
        }
    }
}
