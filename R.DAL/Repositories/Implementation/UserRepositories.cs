using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R.DAL.Context;
using R.DAL.EntityModel;
using R.DAL.Repositories.Interface;

namespace R.DAL.Repositories.Implementation
{
    public class UserRepositories : IUserRepositories
    {
        private readonly RestaurantDbContext _context;

        public UserRepositories(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> AuthenticateUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<List<RoleModel>> GetRoles()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }


        public async Task<bool> IsUsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);

        }

        public async Task<bool> RegisterUser(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task Update(UserModel user)
        {
            user.UpdatedDT = DateTime.UtcNow;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
