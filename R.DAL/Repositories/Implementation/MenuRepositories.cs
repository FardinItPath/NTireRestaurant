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
    public class MenuRepositories : IMenuRepositories
    {
        private readonly RestaurantDbContext _context;

        public MenuRepositories(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuModel>> GetAllMenusAsync()
        {
            return await _context.Menu.Include(m => m.Category).Where(m => m.IsActive).ToListAsync();
        }

        public async Task<MenuModel> GetMenuByIdAsync(int menuId)
        {
            return await _context.Menu.Include(m => m.Category).FirstOrDefaultAsync(m => m.MenuId == menuId && m.IsActive);
        }

        public async Task AddMenuAsync(MenuModel menu)
        {
            await _context.Menu.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuAsync(MenuModel menu)
        {
            _context.Menu.Update(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(int menuId)
        {
            var menu = await _context.Menu.FindAsync(menuId);
            if (menu != null)
            {
                menu.IsActive = false; // Soft delete
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> MenuExistsAsync(int id)
        {
            return await _context.Menu.AnyAsync(m => m.MenuId == id && m.IsActive);
        }
        public async Task<IEnumerable<MenuModel>> GetMenusAsync()
        {
            // Fetch all active menus (without pagination or filtering)
            var menus = await _context.Menu
                .Where(m => m.IsActive)
                .ToListAsync();

            // Map to MenuModel
            var menuModels = menus.Select(m => new MenuModel
            {
                MenuId = m.MenuId,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                IsActive = m.IsActive,
                CategoryId = m.CategoryId
            }).ToList();

            return menuModels;
        }
        public IQueryable<MenuModel> Menus => _context.Menu.Include(m => m.Category);
    }
}

