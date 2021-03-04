using MvvmHelpers;
using Newtonsoft.Json;
using siessi.Settings;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Siessi.Models
{
    /// <summary>
    /// Model for profile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class Profile : ObservableObject
    {
        [JsonIgnore]
        public Action SaveProfileAction { get; set; }

        #region Property

        /// <summary>
        /// Gets or sets the property that has been displays the category.
        /// </summary>
        /// 
        public string category = string.Empty;
        public string Category 
        {
            get => category;
            set => SetProperty(ref category, value, onChanged: SaveProfileAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the category value.
        /// </summary>
        /// 
        public string categoryValue = string.Empty;
        public string CategoryValue 
        {
            get => categoryValue;
            set => SetProperty(ref categoryValue, value, onChanged: SaveProfileAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the category image.
        /// </summary>
        /// 
        public string imagePath = string.Empty;
        public string ImagePath
        { 
            get => imagePath;
            set=> SetProperty(ref imagePath, value, onChanged:SaveProfileAction);
        }

        //Properties to use in production
        /// <summary>
        /// Gets or sets the property that has been displays the user name.
        /// </summary>
        /// 
        string name = string.Empty;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, onChanged: SaveProfileAction);
        }
        /// <summary>
        /// Gets or sets the property that has been displays the user birthdate.
        /// </summary>
        /// 
        DateTime birthdate = DateTime.Today;
        public DateTime BirthDate
        {
            get => birthdate;
            set => SetProperty(ref birthdate, value, onChanged: SaveProfileAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the user gender.
        /// </summary>
        /// 
        string gender = "Mujer";
        public string Gender
        {
            get => gender;
            set => SetProperty(ref gender, value, onChanged: SaveProfileAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the user password.
        /// </summary>
        /// 

        string password = string.Empty;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }


        /// <summary>
        /// Gets or sets the property that has been displays the user image.
        /// </summary>
        /// 
        public string userimage = AppSettings.UserImage;
        public string UserImage
        {
            get => userimage;
            set => SetProperty(ref userimage, value, onChanged: SaveProfileAction);
        }



        #endregion


    }
}