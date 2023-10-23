using HomeWork_22_2_WPFClient.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_22_2_WPFClient.Interfaces
{
    public interface IRoleUser
    {
        Task<IEnumerable<IdentityRole>> GetRoles(IAppUser appUser);
        Task<List<string>> FindByIdAsync(string Role, IAppUser appUser);
        Task<RoleEditModel> GetEditRole(string id, IAppUser appUser);
        Task<bool> SetEditRole(RoleModificationModel model, IAppUser appUser);
        Task<bool> DeleteRole(string id, IAppUser appUser);
        Task<bool> CreateRole(string name, IAppUser appUser);
    }
}
