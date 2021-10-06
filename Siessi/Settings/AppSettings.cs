using Xamarin.Essentials;

namespace siessi.Settings
{
    public static class AppSettings
    {
        public static bool IsFirstRun
        {
            get => Preferences.Get(nameof(IsFirstRun), true);
            set => Preferences.Set(nameof(IsFirstRun), value);
        }

        public static bool IsTermsAccepted
        {
            get => Preferences.Get(nameof(IsTermsAccepted), false);
            set => Preferences.Set(nameof(IsTermsAccepted), value);
        }

        public static bool HasProfile
        {
            get => Preferences.Get(nameof(HasProfile), false);
            set => Preferences.Set(nameof(HasProfile), value);
        }

        public static string UserImage
        {
            get => Preferences.Get(nameof(UserImage), "user.png");
            set => Preferences.Set(nameof(UserImage), value);
        }

        public static bool UpdateProfile
        {
            get => Preferences.Get(nameof(UpdateProfile), false);
            set => Preferences.Set(nameof(UpdateProfile), value);
        }

        public static string UserPassword
        {
            get => Preferences.Get(nameof(UserPassword), "");
            set => Preferences.Set(nameof(UserPassword), value);
        }

        //google ads id's
        const string defaultAndroidAds = "ca-app-pub-9800707284712065/2081732341";
        public static string AndroidAds
        {
            get => Preferences.Get(nameof(AndroidAds), defaultAndroidAds);
            set => Preferences.Set(nameof(AndroidAds), value);
        }
        const string defaultIosAds = "ca-app-pub-9800707284712065/6272737725";
        public static string IosAds
        {
            get => Preferences.Get(nameof(IosAds), defaultIosAds);
            set => Preferences.Set(nameof(IosAds), value);
        }

        //Synfusion Keys
        const string defaultSyncKey = "NTExNzkyQDMxMzkyZTMzMmUzMEM1bEFkZmtXU0ZmTTdWblNQblNOVCtmTEpDMDdWd3RzOUtRRk5SeE5mMnc9";
        public static string SyncFusionLicense
        {
            get => Preferences.Get(nameof(SyncFusionLicense), defaultSyncKey);
            set => Preferences.Set(nameof(SyncFusionLicense), value);
        }

        //AppCenter Keys
        const string defaultAppCenteriOS = "4ddd87d7-9d70-4e7b-8308-b105f4cec0e0";
        public static string AppCenteriOS
        {
            get => Preferences.Get(nameof(AppCenteriOS), defaultAppCenteriOS);
            set => Preferences.Set(nameof(AppCenteriOS), value);
        }
        const string defaultAppCenterAndroid = "2ceab2d9-a9dd-4e39-b37a-09c1c0275081";
        public static string AppCenterAndroid
        {
            get => Preferences.Get(nameof(AppCenterAndroid), defaultAppCenterAndroid);
            set => Preferences.Set(nameof(AppCenterAndroid), value);
        }
    }
}
