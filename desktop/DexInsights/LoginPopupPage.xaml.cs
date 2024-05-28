using Mopups.Services;
using System.Net.Http.Headers;
using System.Text.Json;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using Mopups.Services;
using SkiaSharp.Extended.UI.Controls;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;
using DexInsights.ViewModels;
using DexInsights.Database;
using DexInsights.DataModels;

namespace DexInsights;

public partial class LoginPopupPage {


     private static readonly string[] stateFields = new string[] {
        "Logged in, you will be redirected soon!",
        "This account is banned permanently.",
        "Your account has been terminated!"
    };

	public LoginPopupPage()
	{
        InitializeComponent();
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e) {
        var uri = new Uri("https://google.com");
        Launcher.OpenAsync(uri);
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e) {
        await LoginEvent();
    }

    private void PasswordEntry_OnCompleted(object sender, EventArgs e) {
        OnLoginButtonClicked(this, e);
    }

    private protected async Task LoginEvent() {
        DbUser user = ManagementHandler.GetUsers().Find(user => user.GetName() == usernameEntry.Text);
        SetLoginSuccessState();
        await Task.Delay(900);
        await Navigation.PushAsync(new MenuPage(new ThemeViewModel(), user), false);
        await MopupService.Instance.PopAsync(false);

        await Task.Delay(200);
        usernameEntry.Text = "";
        passwordEntry.Text = "";
    }

    public void SetLoginSuccessState() {
        successLogin.Text = stateFields[0];
        successLogin.TextColor = Colors.Green;
        successLogin.IsVisible = true;
    }

    protected override void OnAppearing() {
        base.OnAppearing();

        LoginButton.IsEnabled = true;
        usernameStack.IsVisible = true;
        passwordStack.IsVisible = true;
        AutomaticLoginAnimation.IsVisible = false;
        successLogin.IsVisible = false;
        usernameFail.IsVisible = false;
        passwordFail.IsVisible = false;
    }
}