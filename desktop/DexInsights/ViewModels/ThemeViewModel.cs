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
        [ObservableProperty]
        Color themeColor3;

        public ThemeViewModel() {
            this.themeColor = Color.FromArgb("CC890D0D");
            this.themeColor2 = Darken(themeColor, 0.4);
            this.themeColor3 = Darken(themeColor, 0.1);
        }

        public void ChangeColor(Color color) {
            this.ThemeColor = color;
            this.ThemeColor2 = Darken(color, 0.4);
            this.ThemeColor3 = Darken(color, 0.1);
        }
        public Color Darken(Color color, double level) {
            return color.WithLuminosity((float)(color.GetLuminosity() - (color.GetLuminosity() * level)));
        }

        public Color Lighten(Color color, double level) {
            return color.WithLuminosity((float)(color.GetLuminosity() + (color.GetLuminosity() * level)));
        }
    }
}
