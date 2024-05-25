using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexInsights.ViewModels {
    public partial class ManagerMemberViewModel : ObservableObject {
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string role;
        [ObservableProperty]
        private string baseRole;

        public ManagerMemberViewModel(string name, string role) {
            this.name = name;
            this.role = role;
            this.baseRole = role;
        }

        public override string ToString() {
            return $"{this.Name},{this.Role}";
        }
    }
}
