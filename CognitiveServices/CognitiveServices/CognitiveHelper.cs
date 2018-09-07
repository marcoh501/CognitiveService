using Microsoft.ProjectOxford.Common.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServices
{   
    public static class CognitiveHelper
    {
        private static string visionApiRoot = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/";
        private static string computerApiKey = "599b47d2ae654107af5a1b8b15750161";

        private static string faceApiRoot = "https://westeurope.api.cognitive.microsoft.com/face/v1.0/";
        private static string faceApiKey = "0c97c2ec6a624166805dede24da6c7eb";

        private static string textAnalyticsApiKey = "791f4f9064104028b7721eb895592064";


        private static string translateApiRoot = "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0";
        private static string translateApiKey = "8f8af8f8958a490e901dd086ed493d93";


        public static async Task<Microsoft.ProjectOxford.Face.Contract.Face[]> RilevaEmozioni(Stream imgStream)
        {
            var faceClient = new Microsoft.ProjectOxford.Face.FaceServiceClient(faceApiKey, faceApiRoot);

            var faceAttrOpts = new[] { Microsoft.ProjectOxford.Face.FaceAttributeType.Emotion };

            var faeResults = await faceClient.DetectAsync(imgStream, returnFaceAttributes: faceAttrOpts);

            if (faeResults == null || faeResults.Length == 0)
            {
                throw new Exception("Can't detect face");
            }

            return faeResults;
        }

        public static async Task<Microsoft.ProjectOxford.Vision.Contract.AnalysisResult> RilevaPersone(Stream imgStream)
        {
            var visionClient = new Microsoft.ProjectOxford.Vision.VisionServiceClient(computerApiKey, visionApiRoot);

            var visionAttrOpts = new[] { Microsoft.ProjectOxford.Vision.VisualFeature.Faces };

            var visionResult = await visionClient.AnalyzeImageAsync(imgStream, visionAttrOpts);

            if (visionResult == null || visionResult.Faces.Length == 0)
            {
                throw new Exception("Can't detect faces");
            }

            return visionResult;           
        }

        public static async Task<Microsoft.ProjectOxford.Vision.Contract.AnalysisResult> DescriviImmagine(Stream imgStream)
        {
            var visionClient = new Microsoft.ProjectOxford.Vision.VisionServiceClient(computerApiKey, visionApiRoot);

            var visionResult = await visionClient.DescribeAsync(imgStream);

            if (visionResult == null)
            {
                throw new Exception("Can't detect the scene");
            }

            return visionResult;
        }

        public static async Task<Microsoft.ProjectOxford.Vision.Contract.OcrResults> RilevaTesto(Stream imgStream)
        {
            var visionClient = new Microsoft.ProjectOxford.Vision.VisionServiceClient(computerApiKey, visionApiRoot);

            var visionResult = await visionClient.RecognizeTextAsync(imgStream);

            if (visionResult == null)
            {
                throw new Exception("Can't detect the text");
            }

            return visionResult;
        }

        public static async Task<string> TraduciTesto(string testo, string languageTo = "it", string languageFrom = null)
        {
            System.Object[] body = new System.Object[] { new { Text = testo } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                string uri = $"{translateApiRoot}&to={languageTo}";
                if (languageFrom != null)
                    uri += $"&from={languageFrom}";                   

                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);                
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", translateApiKey);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<TranslateResponseModel>>(responseBody);                                
       
                return result[0].translations[0].text;
            }
        }

    }
}
