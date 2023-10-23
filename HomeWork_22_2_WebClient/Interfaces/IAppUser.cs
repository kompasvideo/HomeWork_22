namespace HomeWork_22_2_WebClient.Interfaces
{
    public interface IAppUser
    {
        IEnumerable<AppUser> appUsers { get; set; }
        /// <summary>
        /// Залогинился ли юзер
        /// </summary>
        bool logIn { get; set; }
        string UserName { get; set; }

        IEnumerable<AppUser> GetUsers();
        Task LoadUsers();
        Task<bool> CreateUser(CreateModel model);
        Task<AppUser> GetEditUser(string id);
        Task EditUser(string Id, string Email, string UserName);
        Task DeleteUser(string Id);
        bool Login(LoginModel model);
        void Logout();
        string GetToken();
    }
}
