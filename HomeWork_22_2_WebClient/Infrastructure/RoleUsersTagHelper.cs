using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using HomeWork_22_2_WebClient.Models;

namespace HomeWork_22_2_WebClient.Infrastructure {

    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper {
        IRoleUser roleUser;
        IAppUser appUser;
        public RoleUsersTagHelper(IRoleUser roleUser, IAppUser appUser)
        {
            this.roleUser = roleUser;
            this.appUser = appUser;
        }

        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context,
                TagHelperOutput output) {

            List<string> names = new List<string>();

            names = await roleUser.FindByIdAsync(Role, appUser);
            output.Content.SetContent(names.Count == 0 ?
                "No Users" : string.Join(", ", names));
        }
    }
}
