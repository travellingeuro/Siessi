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
                     Password=string.Empty,
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
               AppSettings.HasProfile = true;              
            }
        }


        public void SavePreviousProfile()
        {
            var previousprofile = barrel.Get<Profile>("profile");
            var now = DateTime.Now.ToString();
            barrel.Add<Profile>($"validuntil_{now}", previousprofile, TimeSpan.FromDays(1260));
        }
    }
}
