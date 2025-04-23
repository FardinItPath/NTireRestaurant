using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.DAL.EntityModel;

namespace R.DAL.Repositories.Interface
{
    public interface IUserRepositories 
    {
        Task<bool> RegisterUser(UserModel user);
        Task<UserModel> AuthenticateUser(string username, string password);
        Task<bool> IsUsernameExists(string username);
        Task<UserModel> GetUserById(int userId);
        Task<UserModel> GetUserByUsername(string username);
        Task<List<UserModel>> GetAllUsers();
        Task<List<RoleModel>> GetRoles();
        Task Update(UserModel user);

    }
}
