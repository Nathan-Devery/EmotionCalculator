using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionCalc2.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace EmotionCalc2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class History : ContentPage
    {
        public History()
        {
            Title = "History";
            InitializeComponent();
            displayData();
        }

        async void displayData()
        {
            DatabaseConnecter<Happiness> connecter = new DatabaseConnecter<Happiness>("https://emotioncalc.azurewebsites.net/");
            List<Happiness> happinessHistory = await connecter.GetTableInformation();

            ObservableCollection<Happiness> happinessList = new ObservableCollection<Happiness>();
            foreach (Happiness hap in happinessHistory)
            {
                happinessList.Add(hap);
            }
           emotionList.ItemsSource = happinessList;  
        }

    }
}