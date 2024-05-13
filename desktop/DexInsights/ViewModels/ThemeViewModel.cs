using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexInsights.ViewModels {
    public partial class ThemeViewModel : ObservableObject {

        [ObservableProperty]
        Color themeColor;

        public ThemeViewModel() {
            this.themeColor = Color.FromArgb("CC0D8921");
        }

        public void ChangeColor(Color color) {
            this.ThemeColor = color;
        }
    }
}
