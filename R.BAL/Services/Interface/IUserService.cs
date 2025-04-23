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

        Task<UserModel> AuthenticateUser(string username, string password);
        Task<List<UserViewModel>> GetAllUsers();
        Task<UserViewModel> GetUserById(int userId);
        Task<UserViewModel> GetUserByUsername(string username); // ✅ Fixed return type
        Task<bool> IsUsernameExists(string username);
        Task<bool> RegisterUser(UserViewModel userViewModel);
        Task<List<RoleModel>> GetRoles();
        Task ResetPassword(ResetPasswordViewModel model);
    }
}
