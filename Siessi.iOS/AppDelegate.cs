using Syncfusion.SfRating.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.Pickers.iOS;
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Siessi.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            SfRatingRenderer.Init();
            SfBusyIndicatorRenderer.Init();
            SfSwitchRenderer.Init();
            SfComboBoxRenderer.Init();
            SfDatePickerRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            SfAvatarViewRenderer.Init();
            SfBadgeViewRenderer.Init();
            SfCardViewRenderer.Init();
            SfListViewRenderer.Init();
            Core.Init();
            SfBorderRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfRotatorRenderer.Init();
            SfButtonRenderer.Init();
            SfGradientViewRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
