using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.DAL.EntityModel;

namespace R.BAL.Services.Interface
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuModel>> GetAllMenusAsync(); // Renamed from GetAllMenuItemsAsync
        Task<MenuModel> GetMenuByIdAsync(int menuId);
        Task AddMenuAsync(MenuModel menu); // Renamed from CreateMenuItemAsync
        Task UpdateMenuAsync(MenuModel menu);
        Task DeleteMenuAsync(int menuId);
    }
}
