using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Siessi.ViewModels.Profile
{
    /// <summary>
    /// ViewModel for addprofile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    class AddProfileViewModel : BaseViewModel
    {
        #region fields
        public Models.Profile Profile { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="AddProfileViewModel" /> class.
        /// </summary>
        public AddProfileViewModel()
        {
            Title = "Modificar Perfil";

            Profile = DataService.GetProfile();
            Profile.SaveProfileAction = SaveProfile;

            this.UpdateCommand = new AsyncCommand(OnUpdateMethod);
        }


        #endregion

        #region Commands
        /// <summary>
        /// Gets the command that is executed when the Modificar button is clicked.
        /// </summary>
        /// 
        public AsyncCommand UpdateCommand { get; }


        #endregion

        #region Methods

        private void SaveProfile()
        {
            DataService.SaveProfile(Profile);
            siessi.Settings.AppSettings.HasProfile = true;
            siessi.Settings.AppSettings.UpdateProfile= true;
        }


        //This method should take you back to the profilePage. Changes are automaticaly saved.
        private async Task OnUpdateMethod()
        {
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
            if (string.IsNullOrWhiteSpace(Profile.Password))
            {
                await DisplayAlert("Contraseña", "Tu eres tonto, que metas una contraseña retrasad@");
                return;
            }
            if (Profile.Password.Length < 4)
            {
                await DisplayAlert("Contraseña", "Pero, ¿no has leído que la contraseña es de al menos 4 caracteres, imbécil?");
                return;
            }            

            try
            {
                IsBusy = true;                
                DataService.SaveProfile(Profile);
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message);
            }
            finally
            {
                IsBusy = false;
                siessi.Settings.AppSettings.UpdateProfile = true;
                await GoToAsync("..");
            }
        }

        #endregion

        #region Properties
        public string SyncCreateText => siessi.Settings.AppSettings.HasProfile ? "Actualizar" : "Crear";
        public ImageSource UserImage => ImageSource.FromFile(Profile.UserImage);

        bool showPasswordEntry;
        public bool ShowPasswordEntry
        {
            get
            {
                var showPasswordEntry = string.IsNullOrWhiteSpace(Profile.Password);
                ShowChangePassword = !showPasswordEntry;
                return showPasswordEntry;
                //for develop purposes
                //return true;
            }
            set => SetProperty(ref showPasswordEntry, value);
        }

        bool showChangePassword;
        public bool ShowChangePassword
        {
            get => showChangePassword;
            set => SetProperty(ref showChangePassword, value);
        }

        



        #endregion
    }
}
