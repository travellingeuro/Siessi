using Siessi.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Siessi.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}