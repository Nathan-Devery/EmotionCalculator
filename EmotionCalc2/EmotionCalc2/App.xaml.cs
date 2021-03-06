﻿using System;
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

            TabbedPage tabPage = new TabbedPage();
            tabPage.Children.Add(new Submit());
            tabPage.Children.Add(new QuestionPage());
            tabPage.Children.Add(new History());

            MainPage = tabPage;
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
