using MvvmHelpers.Commands;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using siessi.Settings;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Siessi.ViewModels.Consent
{
    /// <summary>
    /// ViewModel for review page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class FemalePageViewModel : BaseViewModel
    {
        #region fields
        public Models.Consent Consent { get; }
        public Models.Profile Profile { get; }
        
        #endregion

        #region Constructor

        public  FemalePageViewModel()
        {
            Title = "Da tu consentimiento";

            Consent = DataService.GetConsent();
            Consent.SaveConsentAction = SaveConsent;

            Profile = DataService.GetProfile();

            this.GenerateCommand = new AsyncCommand(this.OnGenerateMethod);
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the value for submit command.
        /// </summary>
        public AsyncCommand GenerateCommand { get; set; }

        #endregion

        #region Methods 


        private async Task<Location> MyLocation()
        {
            Location location= await DataService.GetLocation();
            return location;              
        }


        /// <summary>
        /// Invoked when the Submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async Task OnGenerateMethod()
        {
            bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
            if (isFingerprintAvailable)
            {
                var option = await DisplayOptions("Identifícate", "Contraseña", "Huella");
                switch (option)
                {
                    case "Contraseña":
                        await AuthWithPassword();
                        break;
                    case "Huella":
                        await AuthWithFingerprint();
                        break;
                    default:
                        await DisplayAlert("Identificación errónea", "error en la identificación");
                        break;
                }
            }
            else
            {
                await AuthWithPassword();
            }
        }

        //Method execute a confirmation of the actual password before allow to reset it
        private async Task AuthWithPassword()
        {
            if (IsBusy)
                return;
            string result = await DisplayPromt("Contraseña Actual", "");

            if (string.IsNullOrWhiteSpace(result))
                return;

            if (result != AppSettings.UserPassword)
            {
                await DisplayAlert("Contraseña", "La contraseña es errónea");
                return;
            }

            QRContent();
        }
        //Method execute fingerprint checking before allow to reset the password
        private async Task AuthWithFingerprint()
        {
            AuthenticationRequestConfiguration conf = new AuthenticationRequestConfiguration("Identifícate",
                                                                                             "Para otorgar sólo si es si");

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);
            if (authResult.Authenticated)
            {
                //Success  
                QRContent();
            }
            else
            {
                await DisplayAlert("Error", "Authentication failed");
            }

        }

        private async void QRContent()
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
            if (Profile.Gender != "Mujer")
            {
                await DisplayAlert("Alerta", "Sólo si eres mujer puedes decir sólo si es si");
                return;
            }
            //Crear otorgamiento
            Consent.Name = Profile.Name;
            Consent.BirthDate = Profile.BirthDate;
            Consent.Message = Message;
            try
            {
                IsBusy = true;
                Consent.Location = await MyLocation();
            }
            finally
            {
                IsBusy = false;
            }
            DataService.SaveConsent(Consent);
            Message = string.Empty;
            //Json string to include in the QR code
            QRCode = JsonConvert.SerializeObject(Consent);
            IsQRVisible = true;
            Device.StartTimer(TimeSpan.FromSeconds(60), () =>
            {
                IsQRVisible = false;
                OnPropertyChanged(nameof(IsQRVisible));
                return false; 
            });
        }

        private void SaveConsent()
        {
            DataService.SaveConsent(Consent);
        }

        #endregion

        #region Properties

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public bool IsFemale => Profile.Gender == "Mujer";


        string qrCode = "void";
        public string QRCode
        {
            get => qrCode;
            set => SetProperty(ref qrCode, value);
        }


        bool isQRVisible;
        public bool IsQRVisible
        {
            get => isQRVisible;
            set => SetProperty(ref isQRVisible, value);
        }

        string message = string.Empty;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        #endregion

    }
}
