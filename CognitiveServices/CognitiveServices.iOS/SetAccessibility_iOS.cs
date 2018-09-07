using System;

using CognitiveServices.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SetAccessibility_iOS))]
namespace CognitiveServices.iOS
{
    public class SetAccessibility_iOS : ISetAccessibility
    {
        public SetAccessibility_iOS()
        {
        }

        public void setCameraPermission()
        {
            NSUrl url = new NSUrl(UIApplication.OpenSettingsUrlString);
            UIApplication.SharedApplication.OpenUrl(url);
        }

        public void setTextToSpeech()
        {
            NSUrl url = new NSUrl("App-prefs:root=General&path=ACCESSIBILITY");//UIApplication.OpenSettingsUrlString);
            UIApplication.SharedApplication.OpenUrl(url);
        }
    }
}
