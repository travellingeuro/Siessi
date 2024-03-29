﻿using MarcTron.Plugin;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using siessi.Settings;
using Siessi.Views;
using Siessi.Views.Profile;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Siessi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            CrossMTAdmob.Current.UserPersonalizedAds = true;
            CrossMTAdmob.Current.AdsId = DeviceInfo.Platform == DevicePlatform.iOS ? AppSettings.IosAds : AppSettings.AndroidAds;
            AppCenter.Start($"android={AppSettings.AppCenterAndroid};ios={AppSettings.AppCenteriOS}",
                  typeof(Analytics), typeof(Crashes));

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(AddProfilePage), typeof(AddProfilePage));
            Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
        }

    }
}
