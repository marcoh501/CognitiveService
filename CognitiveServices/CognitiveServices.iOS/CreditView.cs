using System;
using Foundation;
using UIKit;
using ObjCRuntime;

namespace CognitiveServices.iOS
{
    public partial class CreditView : UIView
    {
        public CreditView()
        {
        }

        public static CreditView Instance()
        {
            var arr = NSBundle.MainBundle.LoadNib("CreditView", null, null);
            CreditView view = Runtime.GetNSObject<CreditView>(arr.ValueAt(0));
            view.appImage.Image = UIImage.FromBundle("AppLogo.png");
            return view;
        }

        public CreditView(IntPtr handle) : base(handle)
        {
        }

        [Export("awakeFromNib")]
        public override void AwakeFromNib()
        {

            // Perform any additional setup after loading the view, typically from a nib.
        }


    }
}

