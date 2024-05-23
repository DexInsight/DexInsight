using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Animations;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexInsights.ViewModels {
    public partial class ThemeViewModel : ObservableObject {

        [ObservableProperty]
        Color themeColor;
        [ObservableProperty]
        Color themeColor2;

        public ThemeViewModel() {
            this.themeColor = Color.FromArgb("CC890D0D");
            this.themeColor2 = Darken(themeColor);
        }

        public void ChangeColor(Color color) {
            this.ThemeColor = color;
            this.ThemeColor2 = Darken(color);
        }
        public Color Darken(Color color) {
            return color.WithLuminosity((float)(color.GetLuminosity() - (color.GetLuminosity() * .4)));
        }
    }
}
