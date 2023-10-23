using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Messages;
using HomeWork_22_2_WPFClient.Models;
using HomeWork_22_2_WPFClient.Pages;
using HomeWork_22_2_WPFClient.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class PageAddRoleViewModel
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static IAppUser appUser;
        private static IRoleUser roleUser;
        public static string UserName { get; set; }
        public static string UsersName { get; set; }



        public PageAddRoleViewModel(PageService p_pageService, MessageBus p_messageBus, IAppUser p_appUser, IRoleUser p_roleUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            appUser = p_appUser;
            roleUser = p_roleUser;
        }
        public PageAddRoleViewModel()
        {
        }

        public ICommand ButtonReturnClickCommand
        {
            get
            {
                return new DelegateCommand(() => { pageService.ChangePage(new PageAdmin()); });
            }
        }
        public ICommand ButtonAddRoleClickCommand
        {
            get
            {
                var a = new DelegateCommand(async () =>
                {
                    if (await roleUser.CreateRole(UserName, appUser))
                    {
                        IEnumerable<IdentityRole> roles = null;
                        roles = await roleUser.GetRoles(appUser);
                        await messageBus.SendTo<PageRolesViewModel>(new RolesMessage(roles));
                        pageService.ChangePage(new PageRoles());
                    }
                    else
                    {
                        pageService.ChangePage(new PageError());
                    }
                });
                return a;
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
