using System;
using CognitiveServices.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SetAccessibility_Android))]
namespace CognitiveServices.Droid
{
    public class SetAccessibility_Android : ISetAccessibility
    {
        public SetAccessibility_Android()
        {
        }

        public void setCameraPermission()
        {
            throw new NotImplementedException();
        }

        public void setTextToSpeech()
        {
            throw new NotImplementedException();
        }
    }
}
