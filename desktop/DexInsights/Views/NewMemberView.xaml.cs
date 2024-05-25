using DexInsights.ViewModels;

namespace DexInsights.Views;

public partial class NewMemberView : ContentView
{
    ManagerMemberViewModel mmv;
    public event EventHandler<string> NewMemberEvent;
    private string[] roles = { "Owner", "Admin", "User" };
    public NewMemberView(ManagerMemberViewModel model) {
        InitializeComponent();
        BindingContext = model;
        this.mmv = model;
    }

    private void RoleSaveTapped(object sender, TappedEventArgs e) {
        if (mmv.Name.Length == 0) return;
        NewMemberEvent?.Invoke(this, mmv.ToString());
    }

    private void RoleSwitchTapped(object sender, TappedEventArgs e) {
        int index = Array.IndexOf(roles, mmv.Role);
        if (index == roles.Length - 1) {
            index = 0;
        } else {
            index++;
        }
        mmv.Role = roles[index];
    }
}