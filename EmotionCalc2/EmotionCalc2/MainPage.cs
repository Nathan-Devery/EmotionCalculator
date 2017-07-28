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
    public class MainPage : ContentPage
    {
        Label emotionLabel = new Label();
        Image image = new Image();

        public MainPage()
        {
            Button photoButton = new Button
            {
                Text = "Take Photo and Analyze"
            };
            photoButton.Clicked += loadCamera;

            Content = new StackLayout
            {
                Children = {
                    emotionLabel,
                    photoButton,
                    image
                }
            };
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

            image.Source = ImageSource.FromStream(() =>
            {
                return file.GetStream();
            });


            await JudgeEmotion(file);
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
                        toDisplay += " " + item.Key + ":" + item.Value;
                    }
                }

                emotionLabel.Text = toDisplay;


            }

        }


        
    }
}