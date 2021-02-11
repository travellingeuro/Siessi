using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siessi.Views.Profile
{
    /// <summary>
    /// Page to show the health profile.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilePage" /> class.
        /// </summary>
        public ProfilePage()
        {
            InitializeComponent();
        }
    }
}