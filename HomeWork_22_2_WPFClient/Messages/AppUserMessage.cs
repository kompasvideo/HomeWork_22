using HomeWork_22_2_WPFClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_22_2_WPFClient.Messages
{
    internal class AppUserMessage : IMessage
    {
        public AppUserMessage(AppUser p_User)
        {
            User = p_User;
        }

        public AppUser User { get; set; }
    }
}
