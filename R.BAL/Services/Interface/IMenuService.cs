using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ViewModel;

namespace R.BAL.Services.Interface
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuViewModel>> GetAllMenusAsync();
        Task<MenuViewModel> GetMenuByIdAsync(int menuId);
        Task AddMenuAsync(MenuViewModel menu);
        Task<bool> UpdateMenuAsync(int id, MenuViewModel viewModel);

        Task DeleteMenuAsync(int menuId);
    }
}
