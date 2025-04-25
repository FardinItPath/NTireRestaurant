using Common.DTOs;
using Common.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R.BAL.Services.Interface;
using R.DAL.EntityModel;

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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateMenu([FromBody] MenuDTOs model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _menuService.AddMenuAsync(model);
        return Ok(new { message = "Menu item created successfully." });
    }

    // PUT: api/menu/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateMenu(int id, [FromBody] MenuDTOs model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _menuService.UpdateMenuAsync(id, model);
        if (!result)
            return NotFound(new { message = "Menu item not found." });

        return Ok(model);
    }

    // DELETE: api/menu/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
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

   // GET: api/menu/paginated-menu
   [HttpPost("paginated-menu")]
    public async Task<ActionResult<PagedResultDTOs<MenuDTOs>>> GetPagedMenus([FromQuery] PaginationParamsDTOs pagination)
    {
        var pagedMenu = await _menuService.GetPagedMenu(pagination);
        return Ok(pagedMenu);
    }



}
