using siessi.Settings;
using Siessi.ViewModels;
using Siessi.Views;
using Siessi.Views.Profile;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Siessi
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(AddProfilePage), typeof(AddProfilePage));
            Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
        }

    }
}
