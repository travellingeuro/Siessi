using Siessi.Models;
using Siessi.Views.Profile;
using System;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Models.Profile> cardItems;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfileViewModel" /> class.
        /// </summary>
        public ProfileViewModel()
        {
            Title = "Perfil";

            cardItems = new ObservableCollection<Models.Profile>()
            {
                new Models.Profile()
                {
                    Category = "Calories Eaten",
                    CategoryValue = "13,100",
                    ImagePath = "CaloriesEaten.svg"
                },
                new Models.Profile()
                {
                    Category = "Heart Rate",
                    CategoryValue = "87 BPM",
                    ImagePath = "HeartRate.svg"
                },
                new Models.Profile()
                {
                    Category = "Water Consumed",
                    CategoryValue = "38.6 L",
                    ImagePath = "WaterConsumed.svg"
                },
                new Models.Profile()
                {
                    Category = "Sleep Duration",
                    CategoryValue = "7.3 H",
                    ImagePath = "SleepDuration.svg"
                }
            };

            this.ProfileImage = App.BaseImageUrl + "ProfileImage16.png";
            this.ProfileName = "Lela Cortez";
            this.State = "San Francisco";
            this.Country = "CA";
            this.Age = "35";
            this.Weight = "159 Ibs";
            this.Height = "165 cm";
            this.AddProfileCommand = new Command(OnAddProfile);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Gets the command that is executed when the Modificar button is clicked.
        /// </summary>
        public Command AddProfileCommand { get; }

        #endregion

        #region Methods
        /// <summary>
        /// Invoked when the Modificar button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void OnAddProfile(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddProfilePage));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the health profile items collection.
        /// </summary>
        public ObservableCollection<Models.Profile> CardItems
        {
            get => cardItems;

            set => SetProperty(ref cardItems, value);
        }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public string Height { get; set; }

        #endregion
    }
}