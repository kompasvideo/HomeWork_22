using HomeWork_22_2_WPFClient.Model;
using HomeWork_22_2_WPFClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_22_2_WPFClient.Messages
{
    internal class AppRoleMessage : IMessage
    {
        public AppRoleMessage(MyIdentityRole p_Role)
        {
            Role = p_Role;
        }
        public MyIdentityRole Role { get; set; }
    }
}
