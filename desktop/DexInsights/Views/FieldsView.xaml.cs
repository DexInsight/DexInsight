using DexInsights.ViewModels;

namespace DexInsights.Views;

public partial class FieldsView : ContentView
{
    private ThemeViewModel tvm;
	public FieldsView(ThemeViewModel model)
	{
        InitializeComponent();
        BindingContext = model;
        this.tvm = model;

        AnimateAppearing();
    }

    private void AnimateAppearing() {
        var animation = new Animation {
        { 0, 1, new Animation(v => ((Frame)ContentFrame).TranslationY = v, 200, 0) }
    };
        animation.Commit(((Frame)ContentFrame), "AnimateTranslation", length: 200);
    }
}