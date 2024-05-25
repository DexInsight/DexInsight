using DexInsights.ViewModels;
using DexInsights.Views;

namespace DexInsights;

public partial class MenuPage : ContentPage {
    private ThemeViewModel tvm;
    private string selected = "";
    public MenuPage(ThemeViewModel model)
	{
		InitializeComponent();
        BindingContext = model;
        this.tvm = model;
    }

    private void HoverEndedMenuButton(object sender, PointerEventArgs e) {
        var animation = new Animation {
        { 0, 1, new Animation(v => ((Frame)sender).BackgroundColor = Color.FromRgba(48, 138, 56, v), 0.3, 0) },
        { 0, 1, new Animation(v => ((Frame)sender).TranslationX = v, -20, 0) }
    };

        animation.Commit(((Frame)sender), "AnimateColorAndTranslation", length: 250);
    }
    private void HoverBeganMenuButton(object sender, PointerEventArgs e) {
        var animation = new Animation {
            { 0, 1, new Animation(v => ((Frame)sender).BackgroundColor = Color.FromRgba("8a8a8a99"), start: 0, end: 10000) },
            { 0, 1, new Animation(v => ((Frame)sender).TranslationX = v, 0, -20) }
        };

        animation.Commit(((Frame)sender), "AnimateColorAndTranslation", length: 250);
    }

    private void SettingsButtonClicked(object sender, TappedEventArgs e) {
        if (selected != "settings") {
            ContentPane.Children.RemoveAt(0);
            ContentPane.Children.Add(new SettingsView(tvm));
            selected = "settings";
        }
    }

    private void ManagementButtonClicked(object sender, TappedEventArgs e) {
        if (selected != "management") {
            ContentPane.Children.RemoveAt(0);
            ContentPane.Children.Add(new ManagementView(tvm));
            selected = "management";
        }
    }
}