using Siessi.Views;
using Siessi.Views.Profile;
using Xamarin.Forms;
using MarcTron.Plugin;
using Xamarin.Essentials;
using siessi.Settings;

namespace Siessi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            CrossMTAdmob.Current.UserPersonalizedAds = true;
            CrossMTAdmob.Current.AdsId = DeviceInfo.Platform == DevicePlatform.iOS ? AppSettings.IosAds : AppSettings.AndroidAds;
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(AddProfilePage), typeof(AddProfilePage));
            Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
        }

    }
}
