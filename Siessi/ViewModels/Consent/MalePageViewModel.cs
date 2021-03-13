using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;

namespace Siessi.ViewModels.Consent
{
    public class MalePageViewModel : BaseViewModel
    {
        #region fields
        bool isBusy;
        bool isProcessing = false;
        bool isAnalizing = true;
        bool isScanning = true;
        Result result;
        Models.Consent consent; 
        public Models.Profile Profile { get; }
        
        #endregion

        #region contructor

        public MalePageViewModel()
        {
            Title = "Escanea el código QR para obtener el solo sí es sí";

            Profile = DataService.GetProfile();

            
            this.OnBarcodeScannedCommand = new MvvmHelpers.Commands.Command(OnQrScannedMethod);
        }


        #endregion

        #region Commands
        public MvvmHelpers.Commands.Command OnBarcodeScannedCommand { get; set; }

        #endregion

        #region methods
        private void OnQrScannedMethod()
        {
            if (!isProcessing)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    isProcessing = true;
                    isAnalizing = false;
                    
                    OnPropertyChanged(nameof(IsAnalizing));
                    
                    await ManageConsent(result.Text);

                    isProcessing = false;
                    isAnalizing = true;

                    OnPropertyChanged(nameof(IsAnalizing));

                });
            }
        }

        private async Task  ManageConsent(string text)
        {
            //Checks
            if (IsBusy)
                return;
            if (string.IsNullOrWhiteSpace(Profile.Name))
            {
                await DisplayAlert("Perfil", "Tienes que crear un perfil");
                return;
            }
            if (Profile.BirthDate > DateTime.Today.AddYears(-18))
            {
                await DisplayAlert("Fecha de Nacimiento", "Hay que ser mayor para estas cositas maj@");
                return;
            }
            if (Profile.Gender != "Hombre")
            {
                await DisplayAlert("Alerta", "Sólo si eres hombre puedes recibir un consentimiento");
                return;
            }

            //Manage QR Message
            try
            {
                Consent = JsonConvert.DeserializeObject<Models.Consent>(text);
                DataService.SaveConsent(Consent);
                OnPropertyChanged(nameof(Consent));
                await DisplayAlert("Solo si es si", $"Has recibido solo si es si de {Consent.Name}");
            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "Sólo QR creado con esta App");
                
            }
           

        }




        #endregion

        #region properties
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public bool IsAnalizing
        {
            get => isAnalizing;
            set => SetProperty(ref isAnalizing, value);
        }

        public bool IsScanning
        {
            get => isScanning;
            set => SetProperty(ref isScanning, value);
        }

        public Result Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }

        public Models.Consent Consent
        {
            get => consent;
            set => SetProperty(ref consent, value);
        }

        #endregion
    }
}
