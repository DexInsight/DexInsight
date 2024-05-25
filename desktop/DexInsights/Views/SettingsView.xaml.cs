using DexInsights.ViewModels;

namespace DexInsights.Views;

public partial class SettingsView : ContentView
{
    private ThemeViewModel tvm;
	public SettingsView(ThemeViewModel model)
	{
		InitializeComponent();
        BindingContext = model;
        this.tvm = model;

        AnimateAppearing();
	}

    private void ChangeThemeColor(object sender, TappedEventArgs e) {
        Border? border = sender as Border;
        tvm.ChangeColor(border.BackgroundColor);
    }

    private void AnimateAppearing() {
        var animation = new Animation {
        { 0, 1, new Animation(v => ((Frame)ContentFrame).TranslationY = v, 200, 0) }
    };
        animation.Commit(((Frame)ContentFrame), "AnimateTranslation", length: 200);
    }
}