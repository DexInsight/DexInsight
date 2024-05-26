using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using DexInsights.Views;
using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;
using SkiaSharp.Views.Maui.Controls.Hosting;
using UraniumUI;

namespace DexInsights {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiApp<App>().UseMauiCommunityToolkitCore()
                .UseMauiCommunityToolkit()
                .ConfigureMopups()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                })
                .UseSkiaSharp()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureMauiHandlers(handlers => {
                    handlers.AddInputKitHandlers();
                });

            builder.Services.AddSingleton<MenuPage>();
            builder.Services.AddSingleton<SettingsView>();
            builder.Services.AddSingleton<FieldsView>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
