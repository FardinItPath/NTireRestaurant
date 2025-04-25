using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTOs;
using Common.ViewModel;
using R.DAL.EntityModel;

namespace R.BAL.Services.Interface
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuDTOs>> GetAllMenusAsync();
        Task<MenuDTOs> GetMenuByIdAsync(int menuId);
        Task AddMenuAsync(MenuDTOs menu);
        Task<bool> UpdateMenuAsync(int id, MenuDTOs viewModel);

        Task DeleteMenuAsync(int menuId);
        Task<PagedResultDTOs<MenuDTOs>> GetPagedMenu(PaginationParamsDTOs pagination);

    }
}
