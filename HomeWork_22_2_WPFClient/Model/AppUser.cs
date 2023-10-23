using Microsoft.AspNetCore.Identity;
using System;

namespace HomeWork_22_2_WPFClient.Models {

    public class AppUser : IdentityUser, IComparable
    {
        // no additional members are required
        // for basic Identity installation
        public int CompareTo(object? obj)
        {
            AppUser temp = obj as AppUser;
            if (temp != null)
            {
                return this.UserName.CompareTo(temp.UserName);
            }
            return 0;
        }
    }
}
