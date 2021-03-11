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
            Profile.SaveProfileAction = SaveProfile;

            
            this.UploadCommand = new Command<object>(this.OnUploadTapped);
            this.GenerateCommand = new Command<object>(this.OnGenerateMethod);
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the value for upload command.
        /// </summary>
        public Command<object> UploadCommand { get; set; }

        /// <summary>
        /// Gets or sets the value for submit command.
        /// </summary>
        public Command<object> GenerateCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Upload button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void OnUploadTapped(object obj)
        {
            // Do something
        }


        private async Task<Location> MyLocation()
        {
            Location location= await DataService.GetLocation();
            return location;
              
        }


        /// <summary>
        /// Invoked when the Submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void OnGenerateMethod(object obj)
        {
            //Checks
            if (IsBusy)
                return;
            if (string.IsNullOrWhiteSpace(Profile.Name))
            {
                await DisplayAlert("Nombre", "Qué pasa, que no tienes nombre,¿no?");
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
        }

        private void SaveConsent()
        {
            DataService.SaveConsent(Consent);

        }

        private void SaveProfile()
        {
            DataService.SaveProfile(Profile);
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

        

        

        #endregion

    }
}
