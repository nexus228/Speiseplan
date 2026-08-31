
using Speiseplan.Model;
using System.Net.Http.Json;


namespace Speiseplan.Services
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient _httpClient;

        private List<Menu> _menusList;

        public List<Menu> MenuList
        {
            get => _menusList;
            private set
            {
                if (_menusList == value)
                    return;
                _menusList = value;
                MenuListHasChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public MenuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _menusList = new List<Menu>();
        }

        public event EventHandler? MenuListHasChanged;

        public async Task<bool> CreateMenu(string name, DateTime startDate, DateTime endDate)
        {
            bool returnValue = false;

            Menu menuToCreate = new Menu { Name = name, StartDate = startDate, EndDate = endDate };
            
            var response = await _httpClient.PostAsJsonAsync("api/menu", menuToCreate);
            if (response.IsSuccessStatusCode)
            {
                returnValue = true;
            }
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine(error);
            }
            return returnValue;
        }

        public async Task GetAllMenusAsync()
        {
            List<Menu>? menus = await _httpClient.GetFromJsonAsync<List<Menu>>("api/menu/allMenus");
            if (menus == null)
            {
                menus = new List<Menu>();
            }
            MenuList = menus; 
        }

        public async Task<bool> DeleteMenuAsync(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync($"api/menu/{id}");
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}
