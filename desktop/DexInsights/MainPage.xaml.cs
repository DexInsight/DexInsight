using Mopups.Services;

namespace DexInsights {
    public partial class MainPage : ContentPage {

        public MainPage() {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            ShowLoginPopup();
        }

        private void ShowLoginPopup() {
            Task.Factory.StartNew(() => {
                Thread.Sleep(1000);
                MopupService.Instance.PushAsync(new LoginPopupPage());
            });
        }
    }
}
