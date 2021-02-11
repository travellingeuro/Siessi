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

    }
}
