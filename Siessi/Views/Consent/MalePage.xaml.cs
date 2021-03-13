using Siessi.ViewModels.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siessi.Views.Consent
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MalePage : ContentPage
    {
        MalePageViewModel vm;

        public MalePage()
        {
            InitializeComponent();
            BindingContext = vm = new MalePageViewModel();
         }

        protected override bool OnBackButtonPressed()
        {
            if (vm.IsBusy)
                return false;

            return base.OnBackButtonPressed();
        }

        protected override void OnAppearing()
        {
            BarcodeScanView.IsScanning = true;

            base.OnAppearing();            
        }
 
    }
}