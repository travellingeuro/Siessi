using MvvmHelpers.Commands;
using Siessi.Models;
using Siessi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Siessi.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public AsyncCommand<string> GoToCommand { get; }

        public AsyncCommand CloseCommand { get; }

        public BaseViewModel()
        {
            CloseCommand = new AsyncCommand(Close);
            GoToCommand = new AsyncCommand<string>(GoTo);
        }

        Task GoTo(string page) => GoToAsync(page);

        Task Close() =>
            GoToAsync("..");

        //Implements DataService Class
        DataService dataService;
        public DataService DataService => dataService ??= DependencyService.Get<DataService>();

        bool navigating;
        public async Task GoToAsync(string page, string tracker = null)
        {
            if (navigating)
                return;

            await Shell.Current.GoToAsync(page);

            navigating = false;

        }

        //Implements a class to dsiplay Alerts
        public Task DisplayAlert(string title, string message) =>
            Application.Current.MainPage.DisplayAlert(title, message, "OK");

        //Implements a class to display alerts
        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel) =>
            Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);

        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();



        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
