using MvvmHelpers.Commands;
using siessi.Settings;
using Siessi.Models;
using Siessi.Views.Profile;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Siessi.ViewModels.Profile
{
    /// <summary>
    /// ViewModel for profile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProfileViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// To store the health profile data collection.
        /// </summary>
        public ObservableCollection<Models.Profile> CardItems { get; set; }

        public Models.Profile Profile { get; set; }

        public string Picture { get; set; }

        
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfileViewModel" /> class.
        /// </summary>
        public ProfileViewModel()
        {
            Title = "Perfil";

            Profile = DataService.GetProfile();
            Profile.SaveProfileAction = SaveProfile;

            CardItems = PopulateCardItems();

            Picture = AppSettings.UserImage;


            this.AddProfileCommand = new AsyncCommand(OnAddProfile);
            this.FirstRunCommand = new AsyncCommand(OnFirstRun);
        }


        #endregion

        #region Commands
        /// <summary>
        /// Gets the command that is executed when the Modificar button is clicked.
        /// </summary>
        public AsyncCommand AddProfileCommand { get; }

        public AsyncCommand FirstRunCommand { get; }

        #endregion

        #region Methods

        private ObservableCollection<Models.Profile> PopulateCardItems()
        {
           var carditems= new ObservableCollection<Models.Profile>()
            {
                new Models.Profile()
                {
                    Category = "Sexo",
                    CategoryValue = Profile.Gender,
                    ImagePath = Profile.Gender == "Hombre"? "male.png": "female.png"
                },
                new Models.Profile()
                {
                    Category = "Edad",
                    CategoryValue = GetAge(),
                    ImagePath = "age.png"
                }
            };
            return carditems;
        }

        /// <summary>
        /// Invoked when the calcute the age from the birthdate
        /// </summary>   
        private string GetAge()
        {
            var age = DateTime.Today.Year - Profile.BirthDate.Year;
            if (Profile.BirthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age.ToString();
        }

        /// <summary>
        /// Invoked when the Modificar button is clicked.
        /// </summary>       
        private async Task OnAddProfile()
        {
            await GoToAsync(nameof(AddProfilePage));          
        }

        private async Task OnFirstRun()
        {
            if (!AppSettings.HasProfile)
            {
                AppSettings.HasProfile = false;
                await DisplayAlert("¡Bienvenido!", "Bienvenido a _app. Empieza creando tu perfil. No te preocupes, no se compartirá nada a menos que des permiso expreso");
                await GoToAsync(nameof(AddProfilePage));
            }
            else if (AppSettings.UpdateProfile)
            {
                AppSettings.UpdateProfile = false;
                Profile = DataService.GetProfile();               
                Profile.SaveProfileAction = SaveProfile;
                CardItems = PopulateCardItems();
                Picture = AppSettings.UserImage;
                OnPropertyChanged(nameof(Profile));
                OnPropertyChanged(nameof(CardItems));
                OnPropertyChanged(nameof(Picture));
            }
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

       

        #endregion
    }
}