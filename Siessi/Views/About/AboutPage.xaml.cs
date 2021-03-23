using Siessi.ViewModels.About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siessi.Views.About
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        AboutViewModel vm;

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = vm = new AboutViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            if (vm.IsBusy)
                return false;

            return base.OnBackButtonPressed();
        }
    }
}