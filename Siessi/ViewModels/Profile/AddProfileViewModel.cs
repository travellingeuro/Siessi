using MvvmHelpers.Commands;
using siessi.Settings;
using Siessi.Views.Profile;
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
        bool showPasswordEntry;
        bool showChangePassword;

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
            this.ChangePasswordComand = new AsyncCommand(OnChangePasswordMethod);
        }


        #endregion

        #region Commands
        /// <summary>
        /// Gets the command that is executed when the Modificar button is clicked.
        /// </summary>
        /// 
        public AsyncCommand UpdateCommand { get; }
        /// <summary>
        /// Gets the command that is executed when the Cambiar Contrasna button is clicked.
        /// </summary>
        /// 
        public AsyncCommand ChangePasswordComand { get; }



        #endregion

        #region Methods

        private void SaveProfile()
        {
            DataService.SaveProfile(Profile);
            siessi.Settings.AppSettings.HasProfile = true;
            siessi.Settings.AppSettings.UpdateProfile= true;
        }


        //This method should take you back to the profilePage and save the changes.
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

        //This method execute procedure to change the password
        private async Task OnChangePasswordMethod()
        {
            if (IsBusy)
                return;
            string result = await DisplayPromt("Contraseña Actual", "");

            if (string.IsNullOrWhiteSpace(result))
                return;
                
            if(result != AppSettings.UserPassword)
            {
                await DisplayAlert("Contraseña", "La contraseña es errónea");
                return;
            }
            await GoToAsync(nameof(ResetPasswordPage));
        }
        #endregion

        #region Properties
        public string SyncCreateText => siessi.Settings.AppSettings.HasProfile ? "Actualizar" : "Crear";
        public ImageSource UserImage => ImageSource.FromFile(Profile.UserImage);

        
        public bool ShowPasswordEntry
        {
            get
            {
                var showPasswordEntry = string.IsNullOrWhiteSpace(Profile.Password);
                ShowChangePassword = !showPasswordEntry;
                return showPasswordEntry;
            }
            set => SetProperty(ref showPasswordEntry, value);
        }

        
        public bool ShowChangePassword
        {
            get => showChangePassword;
            set => SetProperty(ref showChangePassword, value);
        }

        #endregion
    }
}
