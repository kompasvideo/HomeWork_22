using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Messages;
using HomeWork_22_2_WPFClient.Model;
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
using System.Windows.Input;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class PageEditRoleViewModel
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static IAppUser appUser { get; set; }
        private static IRoleUser roleUser;

        public static MyIdentityRole myIdentityRole { get; set; }
        public static SortedSet<AppUser> AllUsers { get; set; }
        public static ObservableCollection<AppUser> AddUsers { get; set; }
        public static ObservableCollection<AppUser> DelUsers { get; set; }

        public PageEditRoleViewModel(PageService p_pageService, MessageBus p_messageBus,
            IAppUser p_appUser, IRoleUser p_roleUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            appUser = p_appUser;
            roleUser = p_roleUser;
            if (messageBus != null)
            {
                messageBus.Receive<AppRoleMessage>(this, async message =>
                {
                    myIdentityRole = message.Role;
                    AddUsers = new ObservableCollection<AppUser>(message.Role.UsersAdd);
                    DelUsers = new ObservableCollection<AppUser>(message.Role.UsersDel);
                    AllUsers = new SortedSet<AppUser>(message.Role.UsersAdd);
                    foreach(AppUser user in DelUsers)
                    {
                        AllUsers.Add(user);
                    }
                });
            }
        }

        public PageEditRoleViewModel()
        {
        }

        /// <summary>
        /// Нажата кнопка "Вернуться назад"
        /// </summary>
        public ICommand ButtonReturnClickCommand
        {
            get
            {
                return new DelegateCommand(() => { pageService.ChangePage(new PageRoles()); });
            }
        }

        /// <summary>
        /// Нажата кнопка - Удалить (пользователя из роли)
        /// </summary>
        public ICommand ButtonDelClickCommand
        {
            get
            {
                var a = new DelegateCommand(async (obj) =>
                {
                    string name = obj.ToString();
                    AppUser user = null;
                    foreach(AppUser userF in AllUsers)
                    {
                        if (userF.UserName == name)
                        {
                            user = userF;
                        }
                    }
                    try
                    {
                        DelUsers.Remove(user);
                        AddUsers.Add(user);
                    }
                    catch (Exception)
                    {
                        pageService.ChangePage(new PageError());
                    }
                });
                return a;
            }
        }

        /// <summary>
        /// Нажата кнопка - Добавить (пользователя к роли)
        /// </summary>
        public ICommand ButtonAddClickCommand
        {
            get
            {
                var a = new DelegateCommand(async (obj) =>
                {
                    string name = obj.ToString();
                    AppUser user = null;
                    foreach (AppUser userF in AllUsers)
                    {
                        if (userF.UserName == name)
                        {
                            user = userF;
                        }
                    }
                    try
                    {
                        AddUsers.Remove(user);
                        DelUsers.Add(user);
                    }
                    catch (Exception)
                    {
                        pageService.ChangePage(new PageError());
                    }
                });
                return a;
            }
        }

        /// <summary>
        /// Нажата кнопка - Редактировать роль ( происходит запись пользователей для роли в БД)
        /// </summary>
        public ICommand ButtonEditRoleClickCommand
        {
            get
            {
                var a = new DelegateCommand(async (obj) =>
                {
                    RoleModificationModel model = new RoleModificationModel();
                    model.RoleName = myIdentityRole.Name;
                    model.RoleId = myIdentityRole.Id;
                    string[] idsToAdd = new string[AddUsers.Count];
                    string[] idsToDelete = new string[DelUsers.Count];
                    for (int i = 0; i < AddUsers.Count; i++)
                    {
                        idsToAdd[i] = AddUsers[i].Id;
                    }
                    for (int i = 0; i < DelUsers.Count; i++)
                    {
                        idsToDelete[i] = DelUsers[i].Id;
                    }
                    model.IdsToAdd = idsToDelete;
                    model.IdsToDelete = idsToAdd;
                    bool result = await roleUser.SetEditRole(model, appUser);
                    IEnumerable<IdentityRole> roles = await roleUser.GetRoles(appUser);
                    await messageBus.SendTo<PageRolesViewModel>(new RolesMessage(roles.ToList()));
                    pageService.ChangePage(new PageRoles());
                });
                return a;
            }
        }
    }
}
