using MvvmHelpers.Commands;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using siessi.Settings;
using Siessi.Views;
using Siessi.Views.Profile;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Drawing;
using SkiaSharp;

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
        bool showPictureViewer;

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
            this.ChangePictureCommand = new AsyncCommand(OnChangePictureMethod);
            this.PictureTakenCommand = new AsyncCommand<object>(OnImageTakenMethod);
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

        /// <summary>
        /// Gets the command that is executed when the Cambiar Foto de perfil label or badge is clicked.
        /// </summary>
        /// 
        public AsyncCommand ChangePictureCommand { get; }

        /// <summary>
        /// Gets the command that is executed when the photo is taken from the view
        /// </summary>
        /// 
        public AsyncCommand<object> PictureTakenCommand { get; }
        


#endregion

#region Methods

        private void SaveProfile()
        {
            DataService.SaveProfile(Profile);
            siessi.Settings.AppSettings.HasProfile = true;
            siessi.Settings.AppSettings.UpdateProfile= true;
        }

        //this method is executed to change the profile picture
        private async Task OnChangePictureMethod()
        {
            var option = await DisplayOptions("Cambia tu perfil","Hacer una foto", "Elegir una foto");
            switch (option)
            {
                case "Hacer una foto":
                    TakePictureMethod();
                    break;
                case "Elegir una foto":
                    await ChoosePictureMethod();
                    break;
                default:
                    break;
            }
        }

        //This Method navigates to Pphoto Gallery and select a picture
        private async Task ChoosePictureMethod()
        {
            var file = await MediaPicker.PickPhotoAsync();

            if (file != null)

            {
                AppSettings.UserImage = file.FullPath;
                Profile.UserImage = file.FullPath;
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
                await GoToAsync($"//{nameof(AboutPage)}");
            }            

        }

        //This method enables the pictureviewer
        private void TakePictureMethod()
        {
            ShowPictureViewer = true;
        }

        //This mehod takes a picture and store the result in the device and sets the path to the user profile image
        private async Task OnImageTakenMethod(object arg)
        {
            var args = (Xamarin.CommunityToolkit.UI.Views.MediaCapturedEventArgs)arg;
            var rot = args.Rotation;
            var sis = args.Image;  

            var savedPhotoPath= await SavePhotoASync(args.ImageData,rot);
            AppSettings.UserImage = savedPhotoPath;
            Profile.UserImage = savedPhotoPath;
            ShowPictureViewer = false;
            OnPropertyChanged(nameof(UserImage));
        }

        //Awaited method to save the image and return its path
        private async Task<string> SavePhotoASync(byte[] imageData, double rot)
        {
            var storePath = FileSystem.AppDataDirectory;
            var savedPhotoPath = Path.Combine(storePath, "profile.png");
            //Rotoates the image
            SKBitmap original = SKBitmap.Decode(imageData);
            SKBitmap rotated = RotateMethod(original, rot);            
            var dataToEncode = rotated.Encode(SKEncodedImageFormat.Png,100);
            var dataencoded = dataToEncode.ToArray();            

            //Save the Image
            using (var fs = new FileStream(savedPhotoPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                await fs.WriteAsync(dataencoded, 0, dataencoded.Length);
            }
             
            return savedPhotoPath;  
        }       
        
        //Rotate the picture

        private SKBitmap RotateMethod(SKBitmap original, double rot)
        {
            double radians = Math.PI * rot / 180;
            float sine = (float)Math.Abs(Math.Sin(radians));
            float cosine = (float)Math.Abs(Math.Cos(radians));
            int originalWidth = original.Width;
            int originalHeight = original.Height;
            int rotatedWidth = (int)(cosine * originalWidth + sine * originalHeight);
            int rotatedHeight = (int)(cosine * originalHeight + sine * originalWidth);

            var rotated = new SKBitmap(rotatedWidth, rotatedHeight);

            using (var surface = new SKCanvas(rotated))
            {
                surface.Translate(rotatedWidth / 2, rotatedHeight / 2);
                surface.RotateDegrees((float)rot);
                surface.Translate(-originalWidth / 2, -originalHeight / 2);
                surface.DrawBitmap(original, new SKPoint());
            }
            return rotated;
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

        //This method execute procedure to change the password. 
        private async Task OnChangePasswordMethod()
        {

            bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
            if (isFingerprintAvailable)
            {
                var option=await DisplayOptions("Identifícate", "Contraseña", "Huella");
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
                
            if(result != AppSettings.UserPassword)
            {
                await DisplayAlert("Contraseña", "La contraseña es errónea");
                return;
            }
            await GoToAsync(nameof(ResetPasswordPage));
        }
        //Method execute fingerprint checking before allow to reset the password
        private async Task AuthWithFingerprint()
        {
            AuthenticationRequestConfiguration conf =new AuthenticationRequestConfiguration("Identifícate",
                                                                                             "Para acceder al cambio de contraseña");
            
            var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);
            if (authResult.Authenticated)
            {
                //Success  
                await GoToAsync(nameof(ResetPasswordPage));
            }
            else
            {
                await DisplayAlert("Error", "Authentication failed");
            }

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

        public bool ShowPictureViewer
        {
            get => showPictureViewer;
            set => SetProperty(ref showPictureViewer, value);
        }

#endregion
    }
}
