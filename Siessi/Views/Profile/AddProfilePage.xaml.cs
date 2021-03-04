﻿using siessi.Settings;
using Siessi.ViewModels.Profile;
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

        private void DoCameraThings_Clicked(object sender, System.EventArgs e)
        {            
            cameraView.Shutter();              
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