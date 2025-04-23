using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using R.BAL.Services.Interface;
using R.DAL.EntityModel;

namespace NTireRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly ICategoryService _categoryService;

        public MenuController(IMenuService menuService, ICategoryService categoryService)
        {
            _menuService = menuService;
            _categoryService = categoryService;
        }

        // GET: api/menu
        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var menuItems = await _menuService.GetAllMenusAsync();
            return Ok(menuItems);
        }

        // GET: api/menu/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null) return NotFound();
            return Ok(menu);
        }

        // POST: api/menu
        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] MenuModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            model.CreatedDT = DateTime.UtcNow;
            model.UpdatedDT = null;
            model.IsActive = true;

            await _menuService.AddMenuAsync(model);
            return Ok(new { message = "Menu item created successfully." });
        }

        // PUT: api/menu/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] MenuModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _menuService.GetMenuByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.Price = model.Price;
            existing.CategoryId = model.CategoryId;
            existing.IsActive = model.IsActive;
            existing.UpdatedDT = DateTime.UtcNow;

            await _menuService.UpdateMenuAsync(existing);
            return Ok(new { message = "Menu item updated successfully." });
        }

        // DELETE: api/menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null) return NotFound();

            await _menuService.DeleteMenuAsync(id);
            return Ok(new { message = "Menu item deleted successfully." });
        }

        // GET: api/menu/categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            if (categories == null || !categories.Any())
                return NotFound("No categories found.");

            return Ok(categories);
        }
    }
}

