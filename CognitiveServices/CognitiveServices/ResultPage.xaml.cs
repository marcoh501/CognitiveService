using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognitiveServices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        bool reverseTranslate = false;
        public ResultPage() { }

        public ResultPage(MediaFile file, string txt)
        {
            InitializeComponent();

            if (Device.RuntimePlatform != Device.iOS)
                NavigationPage.SetHasNavigationBar(this, false);

            this.img.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

             this.lbl.Text = txt;

              lbl.SetValue(AutomationProperties.HelpTextProperty, txt);

        }

        public ResultPage(MediaFile file, string txt, string tag)
        {

            InitializeComponent();

            if (Device.RuntimePlatform != Device.iOS)
                NavigationPage.SetHasNavigationBar(this, false);

            this.img.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            // list.ItemsSource = (System.Collections.IEnumerable)tags;

            lbl.Text = txt;
            //tagsLabel.Text = tag;
            lbl.SetValue(AutomationProperties.HelpTextProperty, txt);




        }

        public async void btnTraduciClicked(object sender, EventArgs e)
        {
            var translateTo = "it";
            var translateFrom = "en";
            string translatedTxt;
            string translatedtags;
            if (!reverseTranslate){
             translatedTxt = await CognitiveHelper.TraduciTesto(lbl.Text, translateTo, translateFrom);
                reverseTranslate = true;
            }
            else { translatedTxt = await CognitiveHelper.TraduciTesto(lbl.Text, translateFrom, translateTo);
                reverseTranslate = false;
            }

            if (translatedTxt != null)
            {

                lbl.Text = translatedTxt.ToString();
                lbl.SetValue(AutomationProperties.HelpTextProperty, translatedTxt.ToString());
            }
            if (reverseTranslate){
                translatedtags = await CognitiveHelper.TraduciTesto(tagsLabel.Text, translateTo, translateFrom);
                reverseTranslate = false;
            }
            else translatedtags = await CognitiveHelper.TraduciTesto(tagsLabel.Text, translateFrom, translateTo);
            if (translatedtags != null)
                {
                    tagsLabel.Text = translatedtags.ToString();
                    tagsLabel.SetValue(AutomationProperties.HelpTextProperty, translatedtags.ToString());
                reverseTranslate = true;
                }

            
        }
    }
}