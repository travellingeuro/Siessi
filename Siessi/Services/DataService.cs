using MonkeyCache;
using MonkeyCache.FileStore;
using siessi.Settings;
using Siessi.Models;
using Siessi.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
            lock (locker)
            {
                var consent = barrel.Get<Consent>("consent");
                consent ??= new Consent
                {
                    Model=string.Empty,
                    Manufacturer=string.Empty,
                    DeviceName=string.Empty,
                    Location=new Location(),
                    TimeStamp=DateTimeOffset.Now,
                    Name=string.Empty,
                    BirthDate=DateTime.Now
                };
                return consent;
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
            var now = DateTime.Now.ToString();
            barrel.Add<Consent>($"validuntil_{now}", previousconsent, TimeSpan.FromDays(1260));
        }
        #endregion

    }
}
