using DexInsights.ViewModels;
using static Android.Graphics.ColorSpace;

namespace DexInsights.Views;

public partial class FieldsView : ContentView
{
    private ThemeViewModel tvm;
	public FieldsView(ThemeViewModel model)
	{
        InitializeComponent();
        BindingContext = model;
        this.tvm = model;

    }
}