using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork_22.Models
{
    public class EFPhoneBookRepository : IPhoneBookRepository
    {
        private ApplicationDBContext context;

        public EFPhoneBookRepository(ApplicationDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<PhoneBook> PhoneBooks => context.PhoneBooks;

        public PhoneBook DeleteRecord(int phoneBookID)
        {
            PhoneBook dbEntry = context.PhoneBooks.FirstOrDefault(p => p.PhoneBookID == phoneBookID);
            if (dbEntry != null)
            {
                context.PhoneBooks.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveRecord(PhoneBook phoneBook)
        {
            if (phoneBook.PhoneBookID == 0)
            {
                context.PhoneBooks.Add(phoneBook);
            }
            else
            {
                PhoneBook dbEntry = context.PhoneBooks
                    .FirstOrDefault(p => p.PhoneBookID == phoneBook.PhoneBookID);
                if (dbEntry != null)
                {
                    dbEntry.FirstName = phoneBook.FirstName;
                    dbEntry.LastName = phoneBook.LastName;
                    dbEntry.ThreeName = phoneBook.ThreeName;
                    dbEntry.NumberPhone = phoneBook.NumberPhone;
                    dbEntry.Address = phoneBook.Address;
                    dbEntry.Description = phoneBook.Description;                    
                }
            }
            context.SaveChanges();
        }
    }
}
