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
        
        private Models.Consent _selectedConsent;

        public Models.Profile Profile { get; }

        
        public ObservableCollection<Models.Consent> Consents { get;  }

        public Command LoadItemsCommand { get; }
        public Command LoadConsentsCommand { get; }

       
        public Command AddConsentCommand { get; }

        
        public Command<Models.Consent> ConsentTapped { get; }

        public ItemsViewModel()
        {
            Profile = DataService.GetProfile();
            Title = Profile.Gender=="Mujer"? "Consentimientos otorgados": "Consentimientos recibidos";

            
            Consents = new ObservableCollection<Models.Consent>();

            
            LoadConsentsCommand = new Command(async () => await ExecuteLoadConsentsCommand());

            
            ConsentTapped = new Command<Models.Consent>(OnConsentSelected);

           
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


        public void OnAppearing()
        {
            IsBusy = true;
            SelectedConsent = null;          
            
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


        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }


        async void OnConsentSelected(Models.Consent obj)
        {
            if (obj ==null)
                return;
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={obj.Id}");
        }

    }
}