using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.ProjectOxford.Vision;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System;

namespace fw8.CognitiveServices
{
    public class Manager
    {

        public void ProcessingImage(string pathorurl)
        {
            Manager instance = new Manager();
            var client = instance.StartMyTask(pathorurl);
            //var ocr = instance.OCRTask(pathorurl);
            Debug.WriteLine("Caption: " + client.Description.Captions[0].Text);

            //foreach (var a in ocr.Regions[0].Lines)
            //{
            //    foreach (var b in a.Words)
            //    {
            //        Debug.Write(" "+ b.Text);
            //    }
            //}

            foreach (var t in client.Tags)
            {
                double ConfidenceRounded = Math.Round(t.Confidence, 4);
                Debug.WriteLine("Tag: " + t.Name + "  Confidence score: " + ConfidenceRounded);
            }

            foreach (var f in client.Faces)
            {
                Debug.WriteLine("Gender: " + f.Gender + "  Age: " + f.Age.ToString());
            }
        }

        public AnalysisResult StartMyTask(string path)
        {
            Debug.WriteLine("Task started");
            return UploadAndAnalyzeImage(path);
        }

        private static AnalysisResult UploadAndAnalyzeImage(string imageFilePath)
        {
            string SubscriptionKey = ConfigurationManager.AppSettings["SubscriptionKey"].ToString();
            VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey);

            //Checking if the URL is correct
            if (Uri.IsWellFormedUriString(imageFilePath, UriKind.Absolute))
            {
                Debug.WriteLine("URL is correct");
                VisualFeature[] visualFeatures = new VisualFeature[] {
     VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags
    };
                AnalysisResult analysisResult = VisionServiceClient.AnalyzeImageAsync(imageFilePath, visualFeatures).Result;
                return analysisResult;
            }
            else
            {
                Debug.WriteLine("This is not an URL");
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    VisualFeature[] visualFeatures = new VisualFeature[] {
      VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags
     };
                    AnalysisResult analysisResult = VisionServiceClient.AnalyzeImageAsync(imageFileStream, visualFeatures).Result;
                    return analysisResult;
                }
            }
        }

        public OcrResults OCRTask(string path)
        {
            return AnalyzeImageForText(path);
        }

        private static OcrResults AnalyzeImageForText(string imageFilePath)
        {
            Debug.WriteLine("OCRTask started");
            string language = "unk";
            //bool detectOrientation = true;
            string SubscriptionKey = ConfigurationManager.AppSettings["SubscriptionKey"].ToString();
            VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey);
            using (Stream imageFileStream = File.OpenRead(imageFilePath))
            {
                OcrResults ocrResult =  VisionServiceClient.RecognizeTextAsync(imageFileStream, language).Result;
                return ocrResult;
            }
        }
    }
}