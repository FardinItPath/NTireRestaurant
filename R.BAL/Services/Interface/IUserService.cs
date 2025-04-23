using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModel;
using R.DAL.EntityModel;

namespace R.BAL.Services.Interface
{
    public interface IUserService
    {
        object Role { get; }

        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(int userId);
        Task<UserModel> GetUserByUsername(string username);
        Task<bool> IsUsernameExists(string username);
        Task<bool> RegisterUser(UserModel user);
        Task<UserModel> AuthenticateUser(string username, string password);
        Task<List<RoleModel>> GetRoles();
        Task ResetPassword(ResetPasswordViewModel model);
    }
}
