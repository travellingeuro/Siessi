using Siessi.ViewModels.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siessi.Views.Profile
{
    /// <summary>
    /// Page to reset old password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage : ContentPage
    {
        ResetPasswordViewModel vm;

        public ResetPasswordPage()
        {
            InitializeComponent();
            BindingContext = vm = new ResetPasswordViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            if (vm.IsBusy)
                return false;

            return base.OnBackButtonPressed();
        }
    }
}