using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R.BAL.Services.Interface;
using R.DAL.Context;
using R.DAL.EntityModel;

namespace R.BAL.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly RestaurantDbContext _context;

        public CategoryService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Category.FindAsync(categoryId);
        }

        public async Task AddCategoryAsync(CategoryModel category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(CategoryModel category)
        {
            _context.Category.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Category.FindAsync(categoryId);
            if (category != null)
            {
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}

