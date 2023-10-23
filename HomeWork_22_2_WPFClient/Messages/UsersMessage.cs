using HomeWork_22_2_WPFClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_22_2_WPFClient.Messages
{
    public class UsersMessage : IMessage
    {
        public UsersMessage(List<AppUser> p_lAppUsers)
        {
            lAppUsers = p_lAppUsers;
        }

        public List<AppUser> lAppUsers { get; set; }
    }
}