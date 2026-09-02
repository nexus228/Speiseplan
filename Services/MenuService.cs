
using Speiseplan.Model;
using Speiseplan.Services.CustomEventArgs;
using Speiseplan.Services.Enum;
using System.Net.Http.Json;


namespace Speiseplan.Services
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient _httpClient;

        private readonly List<Menu> _menusList = new();

        public IReadOnlyList<Menu> MenuList
        {
            get => _menusList.AsReadOnly();
        }

        public MenuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public event EventHandler<MenusChangedEventArgs>? MenusChanged;

        public async Task<bool> CreateMenu(string name, DateTime startDate, DateTime endDate)
        {
            bool returnValue = false;

            Menu menuToCreate = new Menu { Name = name, StartDate = startDate, EndDate = endDate };
            
            var response = await _httpClient.PostAsJsonAsync("api/menu", menuToCreate);

            if (response.IsSuccessStatusCode)
            {
             
                var createdMenu = await response.Content.ReadFromJsonAsync<Menu>();
                if (createdMenu != null)
                {
                    _menusList.Add(createdMenu);
                    returnValue = true;
                    OnMenusChanged(MenuChangeType.Created, createdMenu);
                }
               
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

            _menusList.Clear();
            _menusList.AddRange(menus);

            OnMenusChanged(MenuChangeType.Loaded);
        }

        public async Task<bool> DeleteMenuAsync(int id)
        {
            bool returnValue = false;

            HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync($"api/menu/{id}");

            if (!httpResponseMessage.IsSuccessStatusCode) 
            {
                return returnValue;
            }

            var menuToDelete = _menusList.FirstOrDefault(m => m.ID == id);

            if(menuToDelete != null) 
            {
                _menusList.Remove(menuToDelete);
                returnValue = true;
                OnMenusChanged(MenuChangeType.Deleted, menuToDelete);
            }

            return returnValue;
        }

        public async Task<bool> UpdateMealAsync(Meal meal)
        {
            bool returnValue = false;
            var response = await _httpClient.PutAsJsonAsync($"api/meal/{meal.Id}", meal);
            
            if (response.IsSuccessStatusCode)
            {
                var updatedMeal = await response.Content.ReadFromJsonAsync<Meal>();
                if (updatedMeal is null)
                    return returnValue;

                int dayId = updatedMeal.DayId;
                int id = updatedMeal.Id;

                var menu = _menusList.FirstOrDefault(m => m.Days.Any(d => d.Id == dayId));
                if (menu != null)
                {
                    var day = menu.Days.FirstOrDefault(d => d.Id == dayId);
                    if (day != null)
                    {
                        var existingMeal = day.Meal.FirstOrDefault(m => m.Id == id);
                        if (existingMeal != null)
                        {
                            // Update the existing meal
                            int indexOfExistingMeal = day.Meal.IndexOf(existingMeal);

                            if (indexOfExistingMeal >= 0)
                                day.Meal[indexOfExistingMeal] = updatedMeal;
                        }
                    }
                } 

                OnMenusChanged(MenuChangeType.Updated, menu);

                returnValue = true;

            }
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine(error);
            }
            return returnValue;
        }



        private void OnMenusChanged(MenuChangeType changeType, Menu? affectedMenu = null)
        {
            MenusChanged?.Invoke(this, new MenusChangedEventArgs(changeType, affectedMenu));
        }
    }
}
