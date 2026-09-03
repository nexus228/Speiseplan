using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Speiseplan.Services;
using Speiseplan.ViewModels;
using Speiseplan.Views;

namespace Speiseplan
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>().UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://menuapi-y9ja.onrender.com/")
            });

            builder.Services.AddSingleton<IMenuService, MenuService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();
            

            builder.Services.AddTransient<StartPageViewModel>();
            builder.Services.AddTransient<MenuPageViewModel>();
            builder.Services.AddTransient<CreateMenuPageViewModel>();
            builder.Services.AddTransient<DayPageViewModel>();
            builder.Services.AddTransient<MealPageViewModel>();
            builder.Services.AddTransient<CurrentDayPageViewModel>();
            builder.Services.AddTransient<EditMealViewModel>();
            builder.Services.AddTransient<ImageGalleryViewModel>();

            builder.Services.AddTransient<StartPage>();
            builder.Services.AddTransient<CurrentDayPage>();
            builder.Services.AddTransient<MenuPage>();
            builder.Services.AddTransient<FavoritesPage>();
            builder.Services.AddTransient<CreateMenuPage>();
            builder.Services.AddTransient<DayPage>();
            builder.Services.AddTransient<MealPage>();
            builder.Services.AddTransient<EditMealPage>();
            builder.Services.AddTransient<ImageGalleryPopup>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var mauiApp = builder.Build();

            return mauiApp;
        }
    }
}
