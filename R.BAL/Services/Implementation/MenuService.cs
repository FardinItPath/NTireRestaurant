using Common.ViewModel;
using R.BAL.Services.Interface;
using R.DAL.EntityModel;
using R.DAL.Repositories.Interface;

public class MenuService : IMenuService
{
    private readonly IMenuRepositories _menuRepository;

    public MenuService(IMenuRepositories menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<IEnumerable<MenuViewModel>> GetAllMenusAsync()
    {
        var menuEntities = await _menuRepository.GetAllMenusAsync();
        return menuEntities.Select(MapToViewModel).ToList();
    }

    public async Task<MenuViewModel> GetMenuByIdAsync(int id)
    {
        var menu = await _menuRepository.GetMenuByIdAsync(id);
        return menu == null ? null : MapToViewModel(menu);
    }

    public async Task AddMenuAsync(MenuViewModel viewModel)
    {
        var entity = MapToEntity(viewModel);
        entity.IsActive = true;
        entity.CreatedDT = DateTime.Now;
        await _menuRepository.AddMenuAsync(entity);
    }

    public async Task<bool> UpdateMenuAsync(int id, MenuViewModel viewModel)
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
    private MenuViewModel MapToViewModel(MenuModel menu)
    {
        return new MenuViewModel
        {
            MenuId = menu.MenuId,
            Name = menu.Name,
            Description = menu.Description,
            Price = menu.Price,
            IsActive = menu.IsActive,
            CategoryId = menu.CategoryId
        };
    }

    private MenuModel MapToEntity(MenuViewModel viewModel)
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
}
