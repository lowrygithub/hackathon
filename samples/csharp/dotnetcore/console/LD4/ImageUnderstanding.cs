
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LieDetector3
{
    class ImageUnderstanding
    {
        private static Dictionary<string, int> lieDict = new Dictionary<string, int>()
            {
                {"smiling",200 },
                {"sugar",1000 },
                {"glasses",50 },
                {"man",50 },
                {"men",500 },
                {"food",500 },
                {"drink",500 },
                {"drinks",500 },
                {"room",50 },
                {"car",500 },


            };

        // subscriptionKey = "0123456789abcdef0123456789ABCDEF"
        private const string subscriptionKey = "a2a234d00c814018bb0a5d5191a11888";

        // localImagePath = @"C:\Documents\LocalImage.jpg"
        private const string localImagePath = @"D:\work and live in Microsoft\picture\2017-09\RQRP0740.jpg";

        private const string remoteImageUrl =
            "https://upload.wikimedia.org/wikipedia/commons/3/3c/Shaki_waterfall.jpg";

        // Specify the features to return
        private static readonly List<VisualFeatureTypes> features =
            new List<VisualFeatureTypes>()
        {
            VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
            VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
            VisualFeatureTypes.Tags,VisualFeatureTypes.Faces,
            VisualFeatureTypes.Adult
        };
        static string ROOT = @"C:\\Hackthon\\";

        static void test(string[] args)
        {
            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { });

            // You must use the same region as you used to get your subscription
            // keys. For example, if you got your subscription keys from westus,
            // replace "westcentralus" with "westus".
            //
            // Free trial subscription keys are generated in the "westus"
            // region. If you use a free trial subscription key, you shouldn't
            // need to change the region.

            // Specify the Azure region
            computerVision.Endpoint = "https://eastasia.api.cognitive.microsoft.com";

            Console.WriteLine("Images being analyzed ...");
            var t1 = AnalyzeRemoteAsync(computerVision, remoteImageUrl);
            var t2 = AnalyzeLocalAsync(computerVision, localImagePath);

            Task.WhenAll(t1, t2).Wait(5000);
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

        public static async Task<Tuple<int,double>> ImageUnderstandingAsync(string imagePathfolder)
        {
            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { });
            computerVision.Endpoint = "https://eastasia.api.cognitive.microsoft.com";
            Dictionary<string, int> tagFreq = new Dictionary<string, int>();
            int index = -1;
            double score = -99;
            for(int i = 0; i < 5; i++)
            {
                string imagePath = imagePathfolder + "\\image_" + i + ".jpg";
                
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine(
                        "\nUnable to open or read localImagePath:\n{0} \n", imagePath);
                    return null;
                }
            
                using (Stream imageStream = File.OpenRead(imagePath))
                {

                    ImageAnalysis analysis = await computerVision.AnalyzeImageInStreamAsync(
                        imageStream, features);
                    if (analysis.Description.Captions.Count != 0)
                    {
                        HashSet<string> tagsSet = new HashSet<string>();
                        foreach (var tag in analysis.Description.Tags)
                        {
                            tagsSet.Add(tag);
                            if (!tagFreq.ContainsKey(tag))
                                tagFreq.Add(tag, 1);
                            else
                                tagFreq[tag] = tagFreq[tag] + 1;
                        }
                        double s = isLyingImage(tagsSet);
                        if (s > score)
                        {
                            score = s;
                            index = i;
                        }
                        //Console.WriteLine(analysis.Description.Captions[0].Text + "\n");
                        //Console.WriteLine(string.Join(",", analysis.Description.Tags) + "\n");
                        //if (analysis.Adult != null)
                        //    Console.WriteLine(analysis.Adult.IsAdultContent + "\n");
                        //Console.WriteLine(analysis.Adult.IsRacyContent + "\n");
                        //if (analysis.Faces.Count > 0)
                        //    Console.WriteLine(analysis.Faces[0].Age + "\n");
                        //Console.WriteLine(analysis.Faces[0].Gender.ToString() + "\n");

                    }
                    //else
                    //{
                    //    Console.WriteLine("No description generated.");
                    //}
                }
            }
            double lying_score = isLyingImage(tagFreq);
            return new Tuple<int, double>(index, lying_score);
            
        }

        private static double isLyingImage(Dictionary<string, int> tagFreq)
        {
            int logit = -100;
            
            foreach (KeyValuePair<string, int> pair in lieDict)
            {
                if (tagFreq.ContainsKey(pair.Key))
                {
                    logit += pair.Value;
                }
            }

            return 100.0 / (1.0 + Math.Exp(-logit / 10));
        }
        private static double isLyingImage(HashSet<string> tagFreq)
        {
            int logit = -100;

            foreach (KeyValuePair<string, int> pair in lieDict)
            {
                if (tagFreq.Contains(pair.Key))
                {
                    logit += pair.Value;
                }
            }

            return 100.0 / (1.0 + Math.Exp(-logit / 10));
        }
        // Analyze a remote image

        private static async Task AnalyzeRemoteAsync(
            ComputerVisionClient computerVision, string imageUrl)
        {
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                Console.WriteLine(
                    "\nInvalid remoteImageUrl:\n{0} \n", imageUrl);
                return;
            }

            ImageAnalysis analysis =
                await computerVision.AnalyzeImageAsync(imageUrl, features);
            DisplayResults(analysis, imageUrl);
        }

        // Analyze a local image
        private static async Task AnalyzeLocalAsync(
            ComputerVisionClient computerVision, string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine(
                    "\nUnable to open or read localImagePath:\n{0} \n", imagePath);
                return;
            }

            using (Stream imageStream = File.OpenRead(imagePath))
            {
                ImageAnalysis analysis = await computerVision.AnalyzeImageInStreamAsync(
                    imageStream, features);
                DisplayResults(analysis, imagePath);
            }
        }

        // Display the most relevant caption for the image
        private static void DisplayResults(ImageAnalysis analysis, string imageUri)
        {
            Console.WriteLine(imageUri);
            if (analysis.Description.Captions.Count != 0)
            {
                Console.WriteLine(analysis.Description.Captions[0].Text + "\n");
                Console.WriteLine(string.Join(",", analysis.Description.Tags) + "\n");
                if (analysis.Adult != null)
                    Console.WriteLine(analysis.Adult.IsAdultContent + "\n");
                Console.WriteLine(analysis.Adult.IsRacyContent + "\n");
                if (analysis.Faces.Count > 0)
                    Console.WriteLine(analysis.Faces[0].Age + "\n");
                Console.WriteLine(analysis.Faces[0].Gender.ToString() + "\n");

            }
            else
            {
                Console.WriteLine("No description generated.");
            }
        }
    }
}