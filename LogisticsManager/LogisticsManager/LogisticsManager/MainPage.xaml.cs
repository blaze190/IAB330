using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogisticsManager
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Tasks_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Tasks());
        }

        private void Login_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.LogIn());
        }

        private void Report_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Report());
        }

        private void Request_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Request());
        }
    }
}
