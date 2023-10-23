using DevExpress.Mvvm;
using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Messages;
using HomeWork_22_2_WPFClient.Models;
using HomeWork_22_2_WPFClient.Pages;
using HomeWork_22_2_WPFClient.Services;
using Microsoft.AspNetCore.Identity;
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
    public class PageAdminViewModel : ViewModelBase
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static IAppUser appUser;
        private static IRoleUser roleUser;

        public static ObservableCollection<AppUser> Users { get; set; }
        public static List<AppUser> lAppUsers;
        public static string UserName { get; set; }

        public  PageAdminViewModel(PageService p_pageService, MessageBus p_messageBus,
            IAppUser p_appUser, IRoleUser p_roleUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            appUser = p_appUser;
            roleUser = p_roleUser;
            if (messageBus != null)
            {
                messageBus.Receive<UsersMessage>(this, async message => { lAppUsers = message.lAppUsers; });
                if (lAppUsers != null)
                    Users = new ObservableCollection<AppUser>(lAppUsers);
            }
            UserName = appUser.UserName;
        }
        public PageAdminViewModel()
        {
        }

        /// <summary>
        ///  Переход на Записную книжку
        /// </summary>
        public ICommand ButtonPhoneRecordClickCommand
        {
            get
            {
                return new DelegateCommand((obj) => { pageService.ChangePage(new Page1AndLoginUser()); });
            }
        }

        /// <summary>
        ///  Переход на Роли
        /// </summary>
        public ICommand ButtonRolesClickCommand
        {
            get
            {
                return new DelegateCommand(async (obj) => 
                {
                    IEnumerable<IdentityRole> roles = await roleUser.GetRoles(appUser);
                    await messageBus.SendTo<PageRolesViewModel>(new RolesMessage(roles.ToList()));
                    pageService.ChangePage(new PageRoles()); 
                });
            }
        }

        /// <summary>
        /// Нажата кнопка - Создать
        /// </summary>
        public ICommand AddUser
        {
            get
            {
                return new DelegateCommand((obj) => { pageService.ChangePage(new PageAddUser()); });
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
        /// Нажата кнопка - Удалить
        /// </summary>
        public ICommand ButtonDelClickCommand
        {
            get
            {
                var a = new DelegateCommand(async (obj) =>
                {
                    string id = obj.ToString();
                    try
                    {
                        await appUser.DeleteUser(id);
                    }
                    catch (Exception )
                    {
                        pageService.ChangePage(new PageError());
                    }
                    await appUser.LoadUsers();
                    var res = appUser.GetUsers().ToList();
                    await messageBus.SendTo<PageAdminViewModel>(new UsersMessage(res));
                    pageService.ChangePage(new PageAdmin());
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
                    string id = obj.ToString();
                    var r = Users.Where(g => g.Id == id).FirstOrDefault();
                    await messageBus.SendTo<PageEditUserViewModel>(new AppUserMessage(r));
                    pageService.ChangePage(new PageEditUser());
                });
                return a;
            }
        }        
    }
}