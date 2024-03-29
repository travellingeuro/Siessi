﻿using MonkeyCache;
using MonkeyCache.FileStore;
using siessi.Settings;
using Siessi.Models;
using Siessi.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(DataService))]
namespace Siessi.Services
{
    public class DataService
    {
        IBarrel barrel;
        object locker = new object();
        CultureInfo myCI;

        [Preserve(AllMembers = true)]
        public DataService()
        {
            Barrel.ApplicationId = AppInfo.PackageName;
            barrel = Barrel.Create(FileSystem.AppDataDirectory);
            myCI = new CultureInfo("es-ES", true);
        }

        #region method_for_Profile       

        public Models.Profile GetProfile()
        {
            lock (locker)
            {
                var profile = barrel.Get<Models.Profile>("profile");
                profile ??= new Models.Profile
                {

                    Category = string.Empty,
                    CategoryValue = string.Empty,
                    ImagePath = string.Empty,
                    Name = string.Empty,
                    BirthDate = DateTime.Today,
                    Password = AppSettings.UserPassword,
                    Gender = "Mujer",
                    UserImage = AppSettings.UserImage
                };
                return profile;
            }
        }

        public void SaveProfile(Models.Profile profile)
        {
            lock (locker)
            {
                if (barrel.Exists("profile"))
                {
                    SavePreviousProfile();
                }
                barrel.Add<Models.Profile>("profile", profile, TimeSpan.FromDays(1260));
                AppSettings.UserPassword = profile.Password;
                AppSettings.HasProfile = true;
            }
        }


        public void SavePreviousProfile()
        {
            var previousprofile = barrel.Get<Models.Profile>("profile");
            var now = DateTime.Now.ToString();
            barrel.Add<Models.Profile>($"validuntil_{now}", previousprofile, TimeSpan.FromDays(1260));
        }
        #endregion

        #region Methods_for_Consent

        public Consent GetConsent()
        {
            var consent = new Consent
            {
                Model = Xamarin.Essentials.DeviceInfo.Model,
                Manufacturer = Xamarin.Essentials.DeviceInfo.Manufacturer,
                DeviceName = Xamarin.Essentials.DeviceInfo.Name,
                Location = new Location(),
                TimeStamp = DateTimeOffset.Now,
                Id = Guid.NewGuid().ToString()
            };
            return consent;
        }

        public List<Consent> GetConsents()
        {

            lock (locker)
            {
                var consents = new List<Consent>();
                var actives = barrel.GetKeys(CacheState.Active);
                var consentkeys = actives.Where((k) => k.Contains("consent"));
                foreach (var key in consentkeys)
                {
                    var consent = barrel.Get<Consent>(key);
                    consents.Add(consent);
                }
                return consents;

            }

        }

        public void SaveConsent(Consent consent)
        {
            lock (locker)
            {
                if (barrel.Exists("consent"))
                {
                    SavePreviousConsent();
                }
                barrel.Add<Consent>("consent", consent, TimeSpan.FromDays(1260));
            }
        }

        public void SavePreviousConsent()
        {
            var previousconsent = barrel.Get<Consent>("consent");
            var now = DateTimeOffset.Now.ToString();
            barrel.Add<Consent>($"consent_{now}", previousconsent, TimeSpan.FromDays(2260));
        }
        #endregion



        #region Method_for_Location

        public async Task<Location> GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.High,
                        Timeout = TimeSpan.FromMilliseconds(5000)
                    });
                }
                if (location == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Location", "Unable to find location, use default", "OK");
                    location = new Location(0, 0);
                    return location;
                }
                return location;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", fnsEx.Message, "OK");
                var location = new Location(0, 0);
                return location;
            }
            catch (PermissionException pEx)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", pEx.Message, "OK");
                var location = new Location(0, 0);
                return location;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", ex.Message, "OK");
                var location = new Location(0, 0);
                return location;
            }
        }
        #endregion
    }
}
