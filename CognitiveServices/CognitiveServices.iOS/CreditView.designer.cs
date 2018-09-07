// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CognitiveServices.iOS
{
    [Register ("CreditView")]
    partial class CreditView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView appImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel nomeAppLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel optionsLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel sviluppataDaLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel versioneAppLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel voiceOverLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch voiceOverSwitch { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (appImage != null) {
                appImage.Dispose ();
                appImage = null;
            }

            if (nomeAppLabel != null) {
                nomeAppLabel.Dispose ();
                nomeAppLabel = null;
            }

            if (optionsLabel != null) {
                optionsLabel.Dispose ();
                optionsLabel = null;
            }

            if (sviluppataDaLabel != null) {
                sviluppataDaLabel.Dispose ();
                sviluppataDaLabel = null;
            }

            if (versioneAppLabel != null) {
                versioneAppLabel.Dispose ();
                versioneAppLabel = null;
            }

            if (voiceOverLabel != null) {
                voiceOverLabel.Dispose ();
                voiceOverLabel = null;
            }

            if (voiceOverSwitch != null) {
                voiceOverSwitch.Dispose ();
                voiceOverSwitch = null;
            }
        }
    }
}