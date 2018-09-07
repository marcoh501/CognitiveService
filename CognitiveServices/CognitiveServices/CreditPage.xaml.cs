using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Essentials;
namespace CognitiveServices
{
    public partial class CreditPage : ContentPage
    {
        public CreditPage()
        {
           InitializeComponent();
            infolabel.Font = Font.OfSize("Helvetica", 30);
            nomeAppLabel.Font = Font.OfSize("Helvetica", 50);
            optionLabel.Font = Font.OfSize("Helvetica", 30);
            versioneAppLabel.Font = Font.OfSize("Helvetica", 30);
            enableCameraLabel.Font = Font.OfSize("Helvetica", 30);
            enableAccessibilityLabel.Font = Font.OfSize("Helvetica", 30);
           



        }

        public async void btnAccessibilityClicked(object sender, EventArgs e)
        {
             DependencyService.Get<ISetAccessibility>().setTextToSpeech();
        }

        public async void btnCameraClicked(object sender, EventArgs e)
        {
            DependencyService.Get<ISetAccessibility>().setCameraPermission();
        }
    }
}
