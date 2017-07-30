using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmotionCalc2.Model;

using Xamarin.Forms;

namespace EmotionCalc2
{
    public class QuestionPage : ContentPage
    {
        public QuestionPage()
        {
            Title = "Activity";
            Entry enteredActivity = new Entry();

            Label heading = new Label
            {
                Text = "Gauge Activity",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center
            };

            Button queryButton = new Button
            {
                Text = "Submit"
            };

            Label result = new Label()
            {
                Margin = 50,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center
            };

            queryButton.Clicked += displayMean;

            Content = new StackLayout
            {
                Margin = 20,
                Padding = 20,
                Children = {
                    heading,
                    enteredActivity,
                    queryButton,
                    result
                }
            };

            async void displayMean(object sender, EventArgs e)
            {
                DatabaseConnecter<Happiness> connecter = new DatabaseConnecter<Happiness>("https://emotioncalc.azurewebsites.net/");

                List<Happiness> hapList = await connecter.GetTableInformation();

                double sum = 0;
                int count = 0;
                foreach (Happiness hap in hapList)
                {
                    if (hap.tag == enteredActivity.Text) 
                    {
                        sum += hap.happinesslevel;
                        count++;
                    }
                }

                if (count == 0)
                {
                    result.TextColor = Color.Blue;
                    result.Text = "Activity Not Found";
                }
                else
                {
                    double mean = sum / count;
                    if (mean > 0.5)
                    {
                        result.TextColor = Color.Green;
                        result.Text = "Increases Happiness";
                    }
                    else
                    {
                        result.TextColor = Color.Red;
                        result.Text = "Decreases Happiness";
                    }
                }
            }
        }
    }
}