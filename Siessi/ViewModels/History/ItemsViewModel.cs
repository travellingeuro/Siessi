using Siessi.Models;
using Siessi.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Siessi.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;
        private Models.Consent _selectedConsent;

        public ObservableCollection<Item> Items { get; }
        public ObservableCollection<Models.Consent> Consents { get;  }

        public Command LoadItemsCommand { get; }
        public Command LoadConsentsCommand { get; }

        public Command AddItemCommand { get; }
        public Command AddConsentCommand { get; }

        public Command<Item> ItemTapped { get; }
        public Command<Models.Consent> ConsentTapped { get; }

        public ItemsViewModel()
        {
            Title = "Historial de consentimientos";
            Items = new ObservableCollection<Item>();
            Consents = new ObservableCollection<Models.Consent>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadConsentsCommand = new Command(async () => await ExecuteLoadConsentsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
            ConsentTapped = new Command<Models.Consent>(OnConsentSelected);

            AddItemCommand = new Command(OnAddItem);
        }


        async Task ExecuteLoadConsentsCommand()
        {
            IsBusy = true;
            
            try
            {
                Consents.Clear();
                var consents = await ConsentStore.GetItemsAsync(true);
                foreach (var consent in consents)
                {
                    Consents.Add(consent);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedConsent = null;
            SelectedItem = null;
            
        }

        public Models.Consent SelectedConsent
        {
            get => _selectedConsent;
            set
            {
                SetProperty(ref _selectedConsent, value);
                OnConsentSelected(value);
            }
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }


        async void OnConsentSelected(Models.Consent obj)
        {
            if (obj ==null)
                return;
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={obj.Name}");
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}