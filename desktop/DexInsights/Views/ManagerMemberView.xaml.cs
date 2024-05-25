using DexInsights.ViewModels;

namespace DexInsights.Views;

public partial class ManagerMemberView : ContentView
{
	ManagerMemberViewModel mmv;
    public event EventHandler<string> BanClickEvent;
	public event EventHandler<string> SaveClickEvent;

    private string[] roles = { "Owner", "Admin", "User" };
	public ManagerMemberView(ManagerMemberViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
		this.mmv = model;
	}

    private void RoleSaveTapped(object sender, TappedEventArgs e) {
		SaveButton.IsVisible = false;
		SaveClickEvent?.Invoke(this, mmv.ToString());
    }

	private void RoleBanTapped(object sender, TappedEventArgs e) {
        BanClickEvent?.Invoke(this, mmv.Name);
    }

    private void RoleSwitchTapped(object sender, TappedEventArgs e) {
		int index = Array.IndexOf(roles, mmv.Role);
		if (index == roles.Length - 1) {
            index = 0;
        } else {
            index++;
        }
		mmv.Role = roles[index];
		HandleSaveButtonVisibility();
    }
	private void HandleSaveButtonVisibility() {
        if (mmv.BaseRole != mmv.Role) {
			SaveButton.IsVisible = true;
			return;
        }
		SaveButton.IsVisible = false;
    }	
}