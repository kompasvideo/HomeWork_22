using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeWork_22_2_WebApi.Models;

namespace HomeWork_22_2_WebApi.Interfaces
{
    public interface IPhoneBookRepository
    {
        IQueryable<PhoneBook> PhoneBooks { get; }
        PhoneBook DeleteRecord(int phoneBookID);
        void SaveRecord(PhoneBook phoneBook);
    }
}
