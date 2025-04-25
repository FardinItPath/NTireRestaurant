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
     

        Task<UserModel> AuthenticateUser(string username, string password);
        Task<List<UserDTOs>> GetAllUsers();
        Task<UserDTOs> GetUserById(int userId);
        Task<UserDTOs> GetUserByUsername(string username); 
        Task<bool> IsUsernameExists(string username);
        Task<bool> RegisterUser(UserDTOs userViewModel);
        Task<List<RoleModel>> GetRoles();
        Task ResetPassword(ResetPasswordDTOs model);
        //Task<bool> UpdateUser(UserDTOs user);
    }
}
