using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace LieDetector3
{
    class SpeechRecognization
    {
        //static string ROOT = @"C:\\Hackthon\\";
        //static string ROOT = @"C:\\Users\\kerli\\Source\\Repos\\cognitive-services-speech-sdk-master\\samples\\csharp\\sharedcontent\\console\\";
        public static async Task<KeyValuePair<string,double>> ContinuousRecognitionWithFileAsync(string ROOT)
        {
            
            // <recognitionContinuousWithFile>
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var config = SpeechConfig.FromSubscription("bf5861835e614c8eb91f535a737cd623", "eastasia");
            var language = "zh-CN";
            config.SpeechRecognitionLanguage = language;
            var stopRecognition = new TaskCompletionSource<int>();

            // Creates a speech recognizer using file as audio input.
            // Replace with your own audio file name.
            string fullText = "";
            using (var audioInput = AudioConfig.FromWavFileInput(ROOT + "record_16.wav"))
            {
                using (var recognizer = new SpeechRecognizer(config, audioInput))
                {
                    // Subscribes to events.
                    recognizer.Recognizing += (s, e) =>
                    {
                        Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
                    };

                    recognizer.Recognized += (s, e) =>
                    {
                        if (e.Result.Reason == ResultReason.RecognizedSpeech)
                        {
                            Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                            fullText = fullText + e.Result.Text;

                        }
                        else if (e.Result.Reason == ResultReason.NoMatch)
                        {
                            Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                        }
                    };

                    recognizer.Canceled += (s, e) =>
                    {
                        Console.WriteLine($"CANCELED: Reason={e.Reason}");

                        if (e.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }

                        stopRecognition.TrySetResult(0);
                    };

                    recognizer.SessionStarted += (s, e) =>
                    {
                        Console.WriteLine("\n    Session started event.");
                    };

                    recognizer.SessionStopped += (s, e) =>
                    {
                        Console.WriteLine("\n    Session stopped event.");
                        Console.WriteLine("\nStop recognition.");
                        stopRecognition.TrySetResult(0);
                    };

                    // Starts continuous recognition. Uses StopContinuousRecognitionAsync() to stop recognition.
                    await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                    // Waits for completion.
                    // Use Task.WaitAny to keep the task rooted.
                    Task.WaitAny(new[] { stopRecognition.Task });

                    // Stops recognition.
                    await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                }
            }
            //System.IO.File.WriteAllText(@"C:\Users\kerli\Music\lies_0.txt", fullText);
            double score = isLies(fullText);
            if (score < 0.4)
            {
                Console.WriteLine("No lies\nlying score: " + score);
            }
            else if (score < 0.6)
            {
                Console.WriteLine("There may be lies\nlying score: " + score);
            }
            else
            {
                Console.WriteLine("You are lying!!!\nlying score: " + score);
            }
            // </recognitionContinuousWithFile>
            KeyValuePair<string, double> pair = new KeyValuePair<string, double>(fullText,score);
            
            return pair;
        }
        public static double isLies(string text)
        {
            int logit = -100;
            Dictionary<string, int> lieDict = new Dictionary<string, int>()
            {
                {"跟我出去",1000 },
                {"带你去玩",1000 },
                {"糖",10 },
                {"玩具", 10},
                {"玩偶", 10},
                {"我跟你爸爸是好朋友", 50},
                {"我跟你妈妈是好朋友", 50},
                {"我这里有糖吃", 100},
                {"你爸爸有事",100},
                {"你妈妈有事",100},
                {"一会就让你走",1000},
                {"叔叔家",10},
                {"阿姨家",10},
                {"带你找爸爸",1000},
                {"带你找妈妈",1000},
                {"好玩的",100},
                {"好吃的",100},
                {"游戏",10},
                {"肯德基",10},
                {"麦当劳",10},
                {"叔叔不会骗你",500},
                {"阿姨不会骗你",500},
                {"帮个忙",10},
                {"时间不早了",1000},
                {"快点走",1000},
                {"乖",100},
                {"听话",100},
                {"懂事",100},
                {"大人",100},
                {"挨骂",10},
                {"好朋友",42},
                {"找不到爸爸",100},
                {"找不到妈妈",100},
                {"先上车",5000},
                {"电话给我",1000},
                {"手机给我",1000},
                {"你不认识",500}
            };
            foreach (KeyValuePair<string, int> pair in lieDict)
            {
                if (text.Contains(pair.Key))
                {
                    logit += pair.Value;
                }
            }

            return 100.0 / (1.0 + Math.Exp(-logit / 10));
        }

    }

    //static void Main()
    //    {
    //        RecognizeSpeechAsync().Wait();
    //        Console.WriteLine("Please press a key to continue.");
    //        Console.ReadLine();
    //    }
    //}
}