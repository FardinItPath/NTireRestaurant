using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.BAL.Services.Interface;
using R.DAL.EntityModel;
using R.DAL.Repositories.Interface;

namespace R.BAL.Services.Implementation
{
    public class MenuService: IMenuService
    {
        private readonly IMenuRepositories _menuRepository;

        public MenuService(IMenuRepositories menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<IEnumerable<MenuModel>> GetAllMenusAsync()
        {
            return await _menuRepository.GetAllMenusAsync();
        }

        public async Task<MenuModel> GetMenuByIdAsync(int id)
        {
            return await _menuRepository.GetMenuByIdAsync(id);
        }

        public async Task AddMenuAsync(MenuModel menu)
        {
            menu.IsActive = true;
            await _menuRepository.AddMenuAsync(menu);
        }

        public async Task UpdateMenuAsync(MenuModel menu)
        {
            await _menuRepository.UpdateMenuAsync(menu);
        }

        public async Task DeleteMenuAsync(int id)
        {
            await _menuRepository.DeleteMenuAsync(id);
        }
    }
}

