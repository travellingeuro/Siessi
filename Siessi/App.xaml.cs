using siessi.Settings;
using Siessi.Services;
using Siessi.Views;
using Siessi.Views.Onboarding;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siessi
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<ConsentDataStore>();
            if (AppSettings.IsTermsAccepted)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new OnBoardingAnimationPage());
            }
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
