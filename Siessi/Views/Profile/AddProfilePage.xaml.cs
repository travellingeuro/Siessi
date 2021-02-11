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
        public AddProfilePage()
        {
            InitializeComponent();
        }
    }
}