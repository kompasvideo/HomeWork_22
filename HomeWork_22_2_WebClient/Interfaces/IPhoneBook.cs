using HomeWork_22_2_WebClient;

namespace HomeWork_22_2_WebClient.Interfaces
{
    public interface IPhoneBook
    {
        IEnumerable<PhoneBook> phoneBooks { get; set; }
        IEnumerable<PhoneBook> GetPhoneBook();
        PhoneBook GetPhoneBookId( int id);
        Task AddRecord(PhoneBook phoneBook, IAppUser appUser);
        Task EditRecord(PhoneBook phoneBook, IAppUser appUser);
        Task DeleteRecord(int id, IAppUser appUser);
    }
}
