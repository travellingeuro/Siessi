using Siessi.Models;
using Siessi.ViewModels;
using Xamarin.Forms;

namespace Siessi.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}