
using Speiseplan.Model;

namespace Speiseplan.Services
{
    public interface IMenuService
    {
        List<Menu>? MenuList { get; }

        event EventHandler? MenuListHasChanged;

        public Task<bool> CreateMenu(string name, DateTime startDate, DateTime endDate);

        public Task GetAllMenusAsync();

        public Task<bool> DeleteMenuAsync(int id);
    }
}
