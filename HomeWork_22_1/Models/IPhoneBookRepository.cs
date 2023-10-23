using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork_22.Models
{
    public interface IPhoneBookRepository
    {
        IQueryable<PhoneBook> PhoneBooks { get; }
        PhoneBook DeleteRecord(int phoneBookID);
        void SaveRecord(PhoneBook phoneBook);
    }
}
