using MvvmHelpers;
using Newtonsoft.Json;
using System;
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
            set => SetProperty(ref model, value);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the Device Manufacturer.
        /// </summary>
        /// 
        string manufacturer = string.Empty;
        public string Manufacturer
        {
            get => manufacturer;
            set => SetProperty(ref manufacturer, value);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the Device Name.
        /// </summary>
        /// 
        string deviceName = string.Empty;
        public string DeviceName
        {
            get => deviceName;
            set => SetProperty(ref deviceName, value);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the Location.
        /// </summary>
        /// 
        Location location = new Location(0, 0);
        public Location Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the timestamp UTC.
        /// </summary>
        /// 
        DateTimeOffset timeStamp = DateTimeOffset.Now;
        public DateTimeOffset TimeStamp
        {
            get => timeStamp;
            set => SetProperty(ref timeStamp, value);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the Name.
        /// </summary>
        /// 
        string name = string.Empty;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the birthDate
        /// </summary>
        /// 
        DateTime birthDate = DateTime.Now;
        public DateTime BirthDate
        {
            get => birthDate;
            set => SetProperty(ref birthDate, value);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the message
        /// </summary>
        /// 
        string message = string.Empty;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        /// <summary>
        /// Gets or sets the property that has been displays the unique Id
        /// </summary>
        /// 
        string id = string.Empty;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        #endregion
    }
}
