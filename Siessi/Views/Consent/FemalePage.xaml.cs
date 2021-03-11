using Siessi.ViewModels.Consent;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siessi.Views.Consent
{
    /// <summary>
    /// Page to get review from customer
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FemalePage : ContentPage
    {
        FemalePageViewModel vm;

        public FemalePage()
        {
            InitializeComponent();
            BindingContext = vm = new FemalePageViewModel();
            this.ProductImage.Source = App.BaseImageUrl + "Image1.png";
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
    }
}