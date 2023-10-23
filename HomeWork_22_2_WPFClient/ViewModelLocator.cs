using Microsoft.Extensions.DependencyInjection;
using HomeWork_22_2_WPFClient.Services;
using HomeWork_22_2_WPFClient.ViewModel;
using HomeWork_22_2_WPFClient.Pages;
using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Data;

namespace HomeWork_22_2_WPFClient
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainViewModel>();
            services.AddTransient<Page1ViewModel>();
            services.AddTransient<Page1AndLoginUserViewModel>();
            services.AddTransient<PageViewRecordViewModel>();
            services.AddTransient<PageAddRecordViewModel>();
            services.AddTransient<PageEditRecordViewModel>();
            services.AddTransient<PageLoginViewModel>();
            services.AddTransient<PageErrorViewModel>();
            services.AddTransient<PageNotAccessViewModel>();
            services.AddTransient<PageAdminViewModel>();
            services.AddTransient<PageAddUserViewModel>();
            services.AddTransient<PageEditUserViewModel>();
            services.AddTransient<PageRolesViewModel>();
            services.AddTransient<PageAddRoleViewModel>();
            services.AddTransient<PageEditRoleViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<EventBus>();
            services.AddSingleton<MessageBus>();
            services.AddSingleton<IPhoneBook, PhoneBookApi>();
            services.AddSingleton<IAppUser, AppUserApi>();
            services.AddSingleton<IRoleUser, RoleUserApi>();



            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }

        public MainViewModel mainViewModel => _provider.GetRequiredService<MainViewModel>();
        public Page1ViewModel page1 => _provider.GetRequiredService<Page1ViewModel>();
        public Page1AndLoginUserViewModel page1AndLoginUser => _provider.GetRequiredService<Page1AndLoginUserViewModel>();
        public PageViewRecordViewModel pageViewRecord => _provider.GetRequiredService<PageViewRecordViewModel>();
        public PageAddRecordViewModel pageAddRecord => _provider.GetRequiredService<PageAddRecordViewModel>();
        public PageEditRecordViewModel pageEditRecord => _provider.GetRequiredService<PageEditRecordViewModel>();
        public PageLoginViewModel PageLogin => _provider.GetRequiredService<PageLoginViewModel>();
        public PageErrorViewModel pageError => _provider.GetRequiredService<PageErrorViewModel>();
        public PageNotAccessViewModel pageNotAccess => _provider.GetRequiredService<PageNotAccessViewModel>();
        public PageAdminViewModel pageAdmin => _provider.GetRequiredService<PageAdminViewModel>();
        public PageAddUserViewModel pageAddUser => _provider.GetRequiredService<PageAddUserViewModel>();
        public PageEditUserViewModel pageEditUser => _provider.GetRequiredService<PageEditUserViewModel>();
        public PageRolesViewModel pageRoles => _provider.GetRequiredService<PageRolesViewModel>();
        public PageAddRoleViewModel pageAddRole => _provider.GetRequiredService<PageAddRoleViewModel>();
        public PageEditRoleViewModel pageEditRole => _provider.GetRequiredService<PageEditRoleViewModel>();
    }
}
