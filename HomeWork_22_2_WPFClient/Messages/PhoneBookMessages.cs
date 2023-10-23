using HomeWork_22_2_WPFClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_22_2_WPFClient.Messages
{
    public class PhoneBookMessages : IMessage
    {
        public PhoneBookMessages(PhoneBook phoneBook)
        {
            this.phoneBook = phoneBook;
        }

        public PhoneBook phoneBook { get; set; }
    }
}