using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_22_2_WPFClient.Messages
{
    public class RolesMessage : IMessage
    {
        public RolesMessage(IEnumerable<IdentityRole> p_roles)
        {
            Roles = p_roles;
        }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
