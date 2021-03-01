using MvvmHelpers.Commands;
using Siessi.Views.Profile;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Siessi.ViewModels.Profile
{
    /// <summary>
    /// ViewModel for reset password page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ResetPasswordViewModel : BaseViewModel
    {
        #region Fields

        public Models.Profile Profile { get; }

        private string newPassword;

        private string confirmPassword;



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordViewModel" /> class.
        /// </summary>
        public ResetPasswordViewModel()
        {
            Title = "Cambia tu contraseña";

            Profile = DataService.GetProfile();
            Profile.SaveProfileAction = SaveProfile;

            SubmitCommand = new AsyncCommand(OnSubmitMethod,DisableButton);             
        }




        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Submit button is clicked.
        /// </summary>
        public AsyncCommand SubmitCommand { get; set; }


        #endregion

        #region Public property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the new password from user in the reset password page.
        /// </summary>
        public string NewPassword
        {
            get => newPassword;

            set => SetProperty(ref newPassword, value);
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the new password confirmation from the user in the reset password page.
        /// </summary>
        public string ConfirmPassword
        {
            get => confirmPassword;

            set => SetProperty(ref confirmPassword, value);
        }

        #endregion

        #region Methods

        private void SaveProfile()
        {
            DataService.SaveProfile(Profile);
            siessi.Settings.AppSettings.HasProfile = true;
            siessi.Settings.AppSettings.UpdateProfile = true;
        }

        private bool DisableButton(object arg)
        {
            return false;
        }
        /// <summary>
        /// Invoked when the Submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async Task OnSubmitMethod()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmPassword))
            { 
                await DisplayAlert("Error", "La contraseña no puede estar en blanco");
                return;
            }
               
            try
            {
                IsBusy = true;
                Profile.Password = ConfirmPassword;
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
    }
}