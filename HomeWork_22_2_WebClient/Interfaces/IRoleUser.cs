using Microsoft.AspNetCore.Identity;

namespace HomeWork_22_2_WebClient.Interfaces
{
    public interface IRoleUser
    {
        Task<IEnumerable<IdentityRole>>  GetRoles(IAppUser appUser);
        Task<List<string>> FindByIdAsync(string Role, IAppUser appUser);
        Task<RoleEditModel> GetEditRole(string id, IAppUser appUser);
        Task<bool> SetEditRole(RoleModificationModel model, IAppUser appUser);
        Task<bool> DeleteRole(string id, IAppUser appUser);
        Task<bool> CreateRole(string name, IAppUser appUser);
    }
}
