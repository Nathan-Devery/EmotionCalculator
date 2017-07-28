using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EmotionCalc2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new EmotionCalc2.MainPage();
            //MainPage = new EmotionCalc2.QuestionPage();
            MainPage = new EmotionCalc2.History();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
