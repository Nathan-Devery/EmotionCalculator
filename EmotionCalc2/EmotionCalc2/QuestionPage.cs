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
            Label infoLabel = new Label
            {
                Text = "Info placeholder"
            };

            Button queryButton = new Button
            {
                Text = "Get info"
            };
            queryButton.Clicked += displayData;

            Content = new StackLayout
            {
                Children = {
                    infoLabel,
                    queryButton,
                }
            };

            async void displayData(object sender, EventArgs e)
            {
                DatabaseConnecter<Cat> connecter = new DatabaseConnecter<Cat>("https://emotioncalc.azurewebsites.net/");
                connecter.PostInformation(new Cat
                {
                    breed = "postTest",
                });


                List <Cat> catList = await connecter.GetTableInformation();

                String catInfo = "";
                foreach(Cat cat in catList)
                {
                    catInfo += " Breed:" + cat.breed + " created: " + cat.createdAt; 
                }

                infoLabel.Text = catInfo;
            }

        }
    }
}