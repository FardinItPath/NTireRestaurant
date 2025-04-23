using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.DAL.EntityModel;

namespace R.DAL.Repositories.Interface
{
    public interface IMenuRepositories
    {
        Task<IEnumerable<MenuModel>> GetAllMenusAsync();
        Task<MenuModel> GetMenuByIdAsync(int menuId);
        Task AddMenuAsync(MenuModel menu);
        Task UpdateMenuAsync(MenuModel menu);
        Task DeleteMenuAsync(int menuId);
        Task<bool> MenuExistsAsync(int id); // Add this line
    }
}
