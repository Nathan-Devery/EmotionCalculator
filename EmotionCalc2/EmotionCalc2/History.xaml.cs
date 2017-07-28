using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionCalc2.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmotionCalc2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class History : ContentPage
    {
        public History()
        {
            InitializeComponent();
            displayData();
        }

        async void displayData()
        {
            DatabaseConnecter<Happiness> connecter = new DatabaseConnecter<Happiness>("https://emotioncalc.azurewebsites.net/");
            List<Happiness> happinessHistory = await connecter.GetTableInformation();

            List<String> rows = new List<String>();
            foreach(Happiness hap in happinessHistory)
            {
                rows.Add(hap.createdAt + " Happiness Level: " + hap.happinesslevel);
            }
            
            emotionList.ItemsSource = rows;  
        }

    }
}