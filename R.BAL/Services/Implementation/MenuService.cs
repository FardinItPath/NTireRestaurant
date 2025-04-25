using Common.DTOs;
using Common.ViewModel;
using Microsoft.EntityFrameworkCore;
using R.BAL.Services.Interface;
using R.DAL.EntityModel;
using R.DAL.Repositories.Implementation;
using R.DAL.Repositories.Interface;

public class MenuService : IMenuService
{
    private readonly IMenuRepositories _menuRepository;

    public MenuService(IMenuRepositories menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<IEnumerable<MenuDTOs>> GetAllMenusAsync()
    {
        var menuEntities = await _menuRepository.GetAllMenusAsync();
        return menuEntities.Select(MapToViewModel).ToList();
    }

    public async Task<MenuDTOs> GetMenuByIdAsync(int id)
    {
        var menu = await _menuRepository.GetMenuByIdAsync(id);
        return menu == null ? null : MapToViewModel(menu);
    }

    public async Task AddMenuAsync(MenuDTOs viewModel)
    {
        var entity = MapToEntity(viewModel);
        entity.IsActive = true;
        entity.CreatedDT = DateTime.Now;
        await _menuRepository.AddMenuAsync(entity);
    }

    public async Task<bool> UpdateMenuAsync(int id, MenuDTOs viewModel)
    {
        var existingEntity = await _menuRepository.GetMenuByIdAsync(id);
        if (existingEntity == null) return false;

        // Update fields
        existingEntity.Name = viewModel.Name ?? string.Empty;
        existingEntity.Description = viewModel.Description ?? string.Empty;
        existingEntity.Price = viewModel.Price;
        existingEntity.IsActive = viewModel.IsActive;
        existingEntity.CategoryId = viewModel.CategoryId;
        //existingEntity.UpdatedBy = viewModel.UpdatedBy;
        existingEntity.UpdatedDT = DateTime.Now;

        await _menuRepository.UpdateMenuAsync(existingEntity);
        return true;
    }

    public async Task DeleteMenuAsync(int id)
    {
        await _menuRepository.DeleteMenuAsync(id);
    }

    // Manual Mappings
    private MenuDTOs MapToViewModel(MenuModel menu)
    {
        return new MenuDTOs
        {
            MenuId = menu.MenuId,
            Name = menu.Name,
            Description = menu.Description,
            Price = menu.Price,
            IsActive = menu.IsActive,
            CategoryId = menu.CategoryId
        };
    }

    private MenuModel MapToEntity(MenuDTOs viewModel)
    {
        return new MenuModel
        {
            MenuId = viewModel.MenuId,
            Name = viewModel.Name ?? string.Empty,
            Description = viewModel.Description ?? string.Empty,
            Price = viewModel.Price,
            IsActive = viewModel.IsActive,
            CategoryId = viewModel.CategoryId
        };
    }
    
   

    public async Task<PagedResultDTOs<MenuDTOs>> GetPagedMenu(PaginationParamsDTOs pagination)
    {
        var query = _menuRepository.Menus
            .Where(m => m.IsActive)
            .AsQueryable();

        if (!string.IsNullOrEmpty(pagination.SearchTerm))
        {
            string search = pagination.SearchTerm.ToLower();
            query = query.Where(m =>
                m.Name.ToLower().Contains(search) ||
                m.Description.ToLower().Contains(search) ||
                m.Price.ToString().Contains(search) ||
                m.Category.CategoryName.ToLower().Contains(search)
            );
        }

        int totalRecords = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalRecords / pagination.PageSize);

        var menus = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(m => new MenuDTOs
            {
                MenuId = m.MenuId,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                CategoryId = m.CategoryId,
                IsActive = m.IsActive
            })
            .ToListAsync();

        return new PagedResultDTOs<MenuDTOs>
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            Items = menus
        };
    }

}
