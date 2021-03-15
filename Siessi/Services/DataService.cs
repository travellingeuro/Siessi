using MonkeyCache;
using MonkeyCache.FileStore;
using siessi.Settings;
using Siessi.Models;
using Siessi.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataService))]
namespace Siessi.Services
{
    public class DataService
    {
        IBarrel barrel;
        object locker = new object();
        CultureInfo myCI;

        public DataService()
        {
            Barrel.ApplicationId = AppInfo.PackageName;
            barrel = Barrel.Create(FileSystem.AppDataDirectory);
            myCI = new CultureInfo("es-ES", true);
        }

        #region method_for_Profile       

        public Profile GetProfile()
        {
            lock (locker)
            {
                var profile = barrel.Get<Profile>("profile");
                profile ??= new Profile
                {
                    
                     Category= string.Empty,
                     CategoryValue=string.Empty,
                     ImagePath=string.Empty,
                     Name= string.Empty,
                     BirthDate=DateTime.Today,
                     Password=AppSettings.UserPassword,
                     Gender="Mujer",
                     UserImage = AppSettings.UserImage
                };
                return profile;
            }
        }

        public void SaveProfile(Profile profile)
        {
            lock (locker)
            {
               if(barrel.Exists("profile"))
                {
                    SavePreviousProfile();
                }
               barrel.Add<Profile>("profile", profile, TimeSpan.FromDays(1260));
               AppSettings.UserPassword = profile.Password;
               AppSettings.HasProfile = true;              
            }
        }


        public void SavePreviousProfile()
        {
            var previousprofile = barrel.Get<Profile>("profile");
            var now = DateTime.Now.ToString();
            barrel.Add<Profile>($"validuntil_{now}", previousprofile, TimeSpan.FromDays(1260));
        }
        #endregion

        #region methods_for_Consent

        public Consent GetConsent()
        {     
            var consent = new Consent
            {
                Model= DeviceInfo.Model,
                Manufacturer=DeviceInfo.Manufacturer,
                DeviceName=DeviceInfo.Name,
                Location=new Location(),
                TimeStamp=DateTimeOffset.Now                   
            };
            return consent;            
        }

        public async Task<IEnumerable<Models.Consent>> GetConsentsAsync(bool forceRefresh = false)
        {
            List<Models.Consent> consents = new List<Consent>();

            return await Task.FromResult(consents);
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
            barrel.Add<Consent>($"consent_{now}", previousconsent, TimeSpan.FromDays(1260));
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
                         Timeout=TimeSpan.FromMilliseconds(5000)
                    });
                }
                if (location == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Location", "Unable to find location, use default","OK");
                    location = new Location(0, 0);
                    return location;
                }
                return location;                    
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", fnsEx.Message, "OK");
                var location = new Location(0, 0);
                return location;
            }
            catch (PermissionException pEx)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", pEx.Message, "OK");
                var location = new Location(0, 0);
                return location;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", ex.Message, "OK");
                var location = new Location(0, 0);
                return location;
            }
        }
        #endregion
    }
}
