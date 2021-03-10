using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace Siessi.Models
{
    /// <summary>
    /// Model for consent object.
    /// </summary>
    [Preserve(AllMembers = true)]

    public class Consent : ObservableObject
    {
        [JsonIgnore]
        public Action SaveConsentAction { get; set; }

        #region Properties
        /// <summary>
        /// Gets or sets the property that has been displays the Device model.
        /// </summary>
        /// 
        string model = string.Empty;
        public string Model 
        { 
            get => model;
            set => SetProperty(ref model, value, onChanged: SaveConsentAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the Device Manufacturer.
        /// </summary>
        /// 
        string manufacturer = string.Empty;
        public string Manufacturer
        {
            get => manufacturer;
            set => SetProperty(ref manufacturer, value, onChanged: SaveConsentAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the Device Name.
        /// </summary>
        /// 
        string deviceName = string.Empty;
        public string DeviceName
        {
            get => DeviceName;
            set => SetProperty(ref deviceName, value, onChanged: SaveConsentAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the Location.
        /// </summary>
        /// 
        Location location = new Location();
        public Location Location
        {
            get => location;
            set => SetProperty(ref location, value, onChanged: SaveConsentAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the timestamp UTC.
        /// </summary>
        /// 
        DateTimeOffset timeStamp = DateTimeOffset.Now;
        public DateTimeOffset TimeStamp
        {
            get => timeStamp;
            set => SetProperty(ref timeStamp, value, onChanged: SaveConsentAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the Name.
        /// </summary>
        /// 
        string name = string.Empty;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, onChanged: SaveConsentAction);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the birthDate
        /// </summary>
        /// 
        DateTime birthDate = DateTime.Now;
        public DateTime BirthDate
        {
            get=> birthDate;
            set => SetProperty(ref birthDate, value, onChanged: SaveConsentAction);
        }

        #endregion
    }
}
