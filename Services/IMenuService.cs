
using Speiseplan.Model;
using Speiseplan.Services.CustomEventArgs;

namespace Speiseplan.Services
{
    public interface IMenuService
    {
        IReadOnlyList<Menu> MenuList { get; }

        event EventHandler<MenusChangedEventArgs>? MenusChanged;

        public Task<bool> CreateMenu(string name, DateTime startDate, DateTime endDate);

        public Task GetAllMenusAsync();

        public Task<bool> DeleteMenuAsync(int id);

        public Task<bool> UpdateMealAsync(Meal meal);
    }
}
