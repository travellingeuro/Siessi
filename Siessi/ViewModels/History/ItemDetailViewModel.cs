using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Siessi.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string model;
        private string manufacturer;
        private string deviceName;
        private Location location;
        private DateTimeOffset timeSpamp;
        private string name;
        private string message;
        private DateTime birthDate;
        private string id;

        public string Id
        {
            get => id;

            set => SetProperty(ref id, value);
        }


        public string Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }

        public string Manufacturer
        {
            get => manufacturer;
            set => SetProperty(ref manufacturer, value);
        }


        public string DeviceName
        {
            get => deviceName;
            set => SetProperty(ref deviceName, value);
        }


        public Location Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }


        public DateTimeOffset TimeStamp
        {
            get => timeSpamp;
            set => SetProperty(ref timeSpamp, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }


        public DateTime BirthDate
        {
            get => birthDate;
            set => SetProperty(ref birthDate, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public ItemDetailViewModel()
        {
            Title = "Detalle del Consentimiento";
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await ConsentStore.GetItemAsync(itemId);
                Id = item.Id;
                Name = item.Name;
                Message = item.Message;
                Model = item.Model;
                Manufacturer = item.Manufacturer;
                DeviceName = item.DeviceName;
                Location = item.Location;
                TimeStamp = item.TimeStamp;
                BirthDate = item.BirthDate;
            }
            catch (Exception)
            {
                await DisplayAlert("Failed to Load Item", "Failed to Load Item");
            }
        }
    }
}
