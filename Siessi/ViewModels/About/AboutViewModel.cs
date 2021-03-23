using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace Siessi.ViewModels.About
{
    [Preserve(AllMembers = true)]
    public class AboutViewModel:BaseViewModel
    {
        bool isBusy = false;

        public AboutViewModel()
        {
            Title = "Sobre esta _app";
        }

        
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public string Content => Resource.Terms;
    }
}
