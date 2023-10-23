using HomeWork_22_2_WPFClient.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_22_2_WPFClient.Model
{
    public class MyIdentityRole 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Users { get; set; }
        public List<AppUser> UsersAdd { get; set; }
        public List<AppUser> UsersDel { get; set; }
        public MyIdentityRole(string p_Id, string p_Name) 
        {
            Id = p_Id;
            Name = p_Name; 
        }
    }
}
