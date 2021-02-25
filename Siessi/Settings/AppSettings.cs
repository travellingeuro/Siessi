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

    }
}
