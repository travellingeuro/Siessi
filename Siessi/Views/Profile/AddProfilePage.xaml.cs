using Siessi.ViewModels.Profile;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siessi.Views.Profile
{
    /// <summary>
    /// This page used for adding Profile image with Name and phone number.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProfilePage : ContentPage
    {
        AddProfileViewModel vm;

        public AddProfilePage()
        {
            InitializeComponent();
            BindingContext = vm = new AddProfileViewModel();

        }

        protected override bool OnBackButtonPressed()
        {
            if (vm.IsBusy)
                return false;

            return base.OnBackButtonPressed();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void cameraElection_StateChanged(object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            this.cameraView.CameraOptions = (bool)e.NewValue ? CameraOptions.Back : CameraOptions.Front;
        }
    }
}