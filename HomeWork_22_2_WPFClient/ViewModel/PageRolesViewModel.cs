using DevExpress.Mvvm;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class PageRolesViewModel
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static IAppUser appUser;
        private static IRoleUser roleUser;

        public static IEnumerable<IdentityRole> lIdentityRole;
        public static ObservableCollection<MyIdentityRole> Roles { get; set; }
        public static string UserName { get; set; }
        private static bool RunCtor { get; set; }
        

        public PageRolesViewModel(PageService p_pageService, MessageBus p_messageBus,
            IAppUser p_appUser, IRoleUser p_roleUser)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            appUser = p_appUser;
            roleUser = p_roleUser;
            if (messageBus != null)
            {
                messageBus.Receive<RolesMessage>(this, async message => 
                {
                    if (!RunCtor)
                    {
                        RunCtor = true;
                        lIdentityRole = message.Roles;
                        if (lIdentityRole != null)
                        {
                            Roles = new ObservableCollection<MyIdentityRole>();
                            foreach (var role in lIdentityRole)
                            {
                                MyIdentityRole myIdentityRole = new MyIdentityRole(role.Id, role.Name);
                                List<string> names = new List<string>();

                                names = await p_roleUser.FindByIdAsync(role.Id, appUser);
                                myIdentityRole.Users = names.Count == 0 ? "No Users" : string.Join(", ", names);
                                Roles.Add(myIdentityRole);
                            }
                        }
                        RunCtor = false;
                    }
                });
            }
            UserName = appUser.UserName;
        }

        public PageRolesViewModel()
        {
        }

        /// <summary>
        ///  Переход на страницу "Записная книжка"
        /// </summary>
        public ICommand ButtonPhoneRecordClickCommand
        {
            get
            {
                return new DelegateCommand((obj) => { pageService.ChangePage(new Page1AndLoginUser()); });
            }
        }

        /// <summary>
        ///  Переход на страницу "Пользователи"
        /// </summary>
        public ICommand ButtonUsersClickCommand
        {
            get
            {
                return new DelegateCommand((obj) => { pageService.ChangePage(new PageAdmin()); });
            }
        }

        /// <summary>
        /// Нажата кнопка - Создать
        /// </summary>
        public ICommand AddRole
        {
            get
            {
                return new DelegateCommand((obj) => { pageService.ChangePage(new PageAddRole()); });
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
                        bool result = await roleUser.DeleteRole(id, appUser);
                        if (result)
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
        /// Нажата кнопка - Редактировать
        /// </summary>
        public ICommand ButtonEditClickCommand
        {
            get
            {
                var a = new DelegateCommand(async (obj) =>
                {
                    string id = obj.ToString();
                    MyIdentityRole r = Roles.Where(g => g.Id == id).FirstOrDefault();
                    await AddUsersInRole(r);
                    await messageBus.SendTo<PageEditRoleViewModel>(new AppRoleMessage(r));
                    pageService.ChangePage(new PageEditRole());
                });
                return a;
            }
        }

        async Task AddUsersInRole(MyIdentityRole r)
        {
            try
            {
                SortedSet<AppUser> usersAdd = new SortedSet<AppUser>();
                SortedSet<AppUser> usersDel = new SortedSet<AppUser>();
                List<string> names = await roleUser.FindByIdAsync(r.Id, appUser);
                foreach (AppUser user in appUser.GetUsers().ToList())
                {
                    bool isContains = false;
                    foreach (string name in names)
                    {
                        if (user.UserName == name)
                            isContains = true;
                    }
                    if (!isContains)
                    {
                        usersAdd.Add(user);
                    }
                    else
                    {
                        usersDel.Add(user);
                    }
                }
                r.UsersAdd = usersAdd.ToList();
                r.UsersDel = usersDel.ToList();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
