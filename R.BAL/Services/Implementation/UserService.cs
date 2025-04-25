using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModel;
using R.BAL.Services.Interface;
using R.DAL.EntityModel;
using R.DAL.Repositories.Interface;

namespace R.BAL.Services.Implementation
{
    public class UserService: IUserService
    {
        private readonly IUserRepositories _userRepository;

  

        public UserService(IUserRepositories userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> AuthenticateUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null || user.Password != password)
                return null;

            return user;
        }

        public async Task<List<UserDTOs>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users.Select(MapToViewModel).ToList();
        }

        public async Task<UserDTOs> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return user == null ? null : MapToViewModel(user);
        }

        public async Task<UserDTOs> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            return user == null ? null : MapToViewModel(user);
        }


        public async Task<bool> IsUsernameExists(string username)
        {
            return await _userRepository.IsUsernameExists(username);
        }

        public async Task<bool> RegisterUser(UserDTOs viewModel)
        {
            if (await IsUsernameExists(viewModel.Username))
                return false;

            var userModel = MapToEntityModel(viewModel);
            userModel.CreatedDT = DateTime.UtcNow;
            userModel.IsActive = true;

            return await _userRepository.RegisterUser(userModel);
        }

        public async Task<List<RoleModel>> GetRoles()
        {
            return await _userRepository.GetRoles();
        }

        public async Task ResetPassword(ResetPasswordDTOs model)
        {
            var user = await _userRepository.GetUserByUsername(model.UserName);
            if (user != null)
            {
                user.Password = model.NewPassword; // Consider hashing here
                await _userRepository.Update(user);
            }
        }

      

        private UserModel MapToEntityModel(UserDTOs viewModel)
        {
            return new UserModel
            {
                UserId = viewModel.UserId,
                Username = viewModel.Username,
                Password = viewModel.Password,
                IsActive = viewModel.IsActive,
                RoleId = viewModel.RoleId,
                CreatedDT = DateTime.UtcNow,
                UpdatedDT = null
            };
        }

        private UserDTOs MapToViewModel(UserModel user)
        {
            return new UserDTOs
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                IsActive = user.IsActive,
                RoleId = user.RoleId,
                //RoleName = user.Role?.RoleName
            };
        }
    }
}
