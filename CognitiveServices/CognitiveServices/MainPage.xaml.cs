using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CognitiveServices
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            btnEmozione.Font = Font.OfSize("Helvetica", 30);
            btnVolti.Font = Font.OfSize("Helvetica", 30);
            btnTesto.Font = Font.OfSize("Helvetica", 30);
            btnScena.Font = Font.OfSize("Helvetica", 30);
            appName.Font = Font.OfSize("Helvetica", 30);
        }

        internal void showLoading()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.loader.IsVisible = true;
               
                this.btnEmozione.IsVisible = this.btnVolti.IsVisible = this.btnScena.IsVisible = this.btnTesto.IsVisible = false;

            });
        }
        internal void hideLoading()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.loader.IsVisible = false;

                this.btnEmozione.IsVisible = this.btnVolti.IsVisible = this.btnScena.IsVisible = this.btnTesto.IsVisible = true;
            });
        }

        private async Task<MediaFile> takeAPicture(bool frontCamera = false)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                throw new Exception("Nessuna fotocamera disponibile");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                DefaultCamera = frontCamera ? CameraDevice.Front : CameraDevice.Rear,
                SaveToAlbum = false,
                CompressionQuality = 92,
                PhotoSize = PhotoSize.Large
            });

            if (file == null)
                throw new Exception("Nessuna immagine acquisita");

            return file;
        }

        public async void btnEmozioniClicked(object sender, EventArgs e)
        {
            try
            {
                this.showLoading();

                var file = await this.takeAPicture(true);

                var stream = file.GetStream();

                var emotionResult = await CognitiveHelper.RilevaEmozioni(stream);

               // var tradotto = await CognitiveHelper.TraduciTesto(emotionResult[0].FaceAttributes.Emotion.ToRankedList().First().Key);

                this.hideLoading();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.Navigation.PushAsync(new ResultPage(file, emotionResult[0].FaceAttributes.Emotion.ToRankedList().First().Key)); //tradotto));
                });

            }
            catch (Exception ex)
            {
                this.hideLoading();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (ex.Message != "Nessuna immagine acquisita")
                        await DisplayAlert("Si è verificato un errore", ex.Message, "OK");
                });
            }
        }


        public async void btnInfoClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await this.Navigation.PushAsync(new CreditPage()); //tradotto));
            });
        }
        public async void btnPersoneClicked(object sender, EventArgs e)
        {
            try
            {
                this.showLoading();

                var file = await this.takeAPicture(true);

                var stream = file.GetStream();

                var visionResult = await CognitiveHelper.RilevaPersone(stream);

                string stringResult = visionResult.Faces.Count() == 1 ? $"{visionResult.Faces.Count()} person detected: " : $"{visionResult.Faces.Count()} people detected: ";
                string persone = "";
                foreach (var f in visionResult.Faces)
                {
                    if (persone != "")
                        persone += ", ";
                    persone += $"{f.Gender} {f.Age}";
                }                

                stringResult += persone;

               // var tradotto = await CognitiveHelper.TraduciTesto(stringResult, languageFrom: "en");

                this.hideLoading();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.Navigation.PushAsync(new ResultPage(file, stringResult));
                });

            }
            catch (Exception ex)
            {
                this.hideLoading();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (ex.Message != "Nessuna immagine acquisita")
                        await DisplayAlert("Si è verificato un errore", ex.Message, "OK");
                });
            }
        }

        public async void btnDescriviClicked(object sender, EventArgs e)
        {
            try
            {
                this.showLoading();

                var file = await this.takeAPicture();

                var stream = file.GetStream();

                var visionResult = await CognitiveHelper.DescriviImmagine(stream);

                //var tradotto = await CognitiveHelper.TraduciTesto(visionResult.Description.Captions[0].Text);
                var tags = "" ;
                foreach (var item in visionResult.Description.Tags)
                {
                    tags += item+ ", ";
                }
                this.hideLoading();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.Navigation.PushAsync(new ResultPage(file, visionResult.Description.Captions[0].Text,tags));
                });

            }
            catch (Exception ex)
            {
                this.hideLoading();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (ex.Message != "Nessuna immagine acquisita")
                        await DisplayAlert("Si è verificato un errore", ex.Message, "OK");
                });
            }
        }

        public async void btnTestoClicked(object sender, EventArgs e)
        {
            try
            {
                this.showLoading();

                var file = await this.takeAPicture();

                var stream = file.GetStream();

                var visionResult = await CognitiveHelper.RilevaTesto(stream);

                var testoRilevato = "";
                foreach (var bb in visionResult.Regions)
                {
                    foreach (var line in bb.Lines)
                    {
                        testoRilevato += Environment.NewLine;

                        foreach (var word in line.Words)
                        {
                            testoRilevato += word.Text + " ";
                        }
                    }
                }

                this.hideLoading();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.Navigation.PushAsync(new ResultPage(file, testoRilevato));
                });

            }
            catch (Exception ex)
            {
                this.hideLoading();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (ex.Message != "Nessuna immagine acquisita")
                        await DisplayAlert("Si è verificato un errore", ex.Message, "OK");
                });
            }
        }


    }
}
