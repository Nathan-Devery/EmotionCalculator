using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using EmotionCalc2.Model;


namespace EmotionCalc2
{
    public class Submit : ContentPage
    {
        Label emotionLabel = new Label();
        Image image = new Image();
        StackLayout stack;
        Entry tag = new Entry();
        Button photoButton;

        double unpostedHappiness = 0;

        public Submit()
        {
            Title = "Log Happiness";

            photoButton = new Button
            {
                Text = "Take Photo and Analyze"
            };
            photoButton.Clicked += loadCamera;

            stack = new StackLayout()
            {
                Children = {
                    image,
                    emotionLabel,
                    photoButton,
                }
            };

            Content = stack;
        }

        private async void loadCamera(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                Directory = "Sample",
                Name = $"{DateTime.UtcNow}.jpg"
            });

            if (file == null)
                return;

            stack.Children.RemoveAt(2);

            image.Source = ImageSource.FromStream(() =>
            {
                return file.GetStream();
            });

            await JudgeEmotion(file);

            //Photo taken, the following now changes the content to allow tagging & submission

            stack.Children.Add(new Label()
            {
                Text = "Recent Activity Tag:"
            });
            stack.Children.Add(tag);

            Button postBut = new Button()
            {
                Text = "Upload"
            };
            postBut.Clicked += postInfo;

            stack.Children.Add(postBut);
            
        }

        static byte[] GetImageAsByteArray(MediaFile file)
            {
                var stream = file.GetStream();
                BinaryReader binaryReader = new BinaryReader(stream);
                return binaryReader.ReadBytes((int)stream.Length);
            }

        async Task JudgeEmotion(MediaFile file)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4aa9cc8cdb984adeafc0b99c10220d83");

            string uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?";
            HttpResponseMessage response;
            string responseContent;

            byte[] byteData = GetImageAsByteArray(file);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                responseContent = response.Content.ReadAsStringAsync().Result;

                Emotion[] emotions = JsonConvert.DeserializeObject<Emotion[]>(responseContent);

                String toDisplay = "";
                foreach(Emotion emotion in emotions)
                {
                    foreach (var item in emotion.scores)
                    {
                        if(item.Key.Equals("happiness")) {
                            toDisplay += "happiness: " + item.Value;
                            unpostedHappiness = item.Value;
                        }
                    }
                }

                emotionLabel.Text = toDisplay;
            }
        }

        async void postInfo(object sender, EventArgs e)
        {
            DatabaseConnecter<Happiness> connecter = new DatabaseConnecter<Happiness>("https://emotioncalc.azurewebsites.net/");
            await connecter.PostInformation(new Happiness
            {
                happinesslevel = unpostedHappiness
            });

            photoButton = new Button
            {
                Text = "Take Photo and Analyze"
            };
            photoButton.Clicked += loadCamera;

            image = new Image();
            emotionLabel.Text = "";

            stack = new StackLayout()
            {
                Children = {
                    image,
                    emotionLabel,
                    photoButton,
                }
            };

            Content = stack;

        }

       
    }
}