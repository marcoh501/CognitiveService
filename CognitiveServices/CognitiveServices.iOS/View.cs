using Foundation;
using Mono;
using System;
using UIKit;

namespace CognitiveServices.iOS
{
    public partial class View : UIView
    {
        public static View Instance()
        {
            var arr = NSBundle.MainBundle.LoadNib("TravelReason_es_View", null, null);
           // View view = Runtime.GetNSObject<View>(arr.ValueAt(0));

            //return view;
        }

        public View (IntPtr handle) : base (handle)
        {
        }

        [Export("awakeFromNib")]
        public override void AwakeFromNib()
        { }
    }
}