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

            

            clearStack();
            hideButtons();
            unhideButtons();
         
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

        private void Register_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Register());
        }

        private void RegisterUser_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.RegisterUser());
        }

        private void NewsFeed_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.NewsFeed());
        }

        private void Logout_Button_Clicked(object sender, EventArgs e)
        {
            Constants.user = null;
            Constants.company = null;
            Navigation.PushAsync(new MainPage());
        }

        private void hideButtons() {
            buttonLogin.IsVisible = false;
            buttonTasks.IsVisible = false;
            buttonReport.IsVisible = false;
            buttonRequest.IsVisible = false;
            buttonLogout.IsVisible = false;
            buttonRegister.IsVisible = false;
            buttonRegisterUser.IsVisible = false;
        }

        private void unhideButtons()
        {
            if (Constants.user != null)
            {
                buttonTasks.IsVisible = true;
                buttonReport.IsVisible = true;
                buttonRequest.IsVisible = true;
                buttonLogout.IsVisible = true;
                if (Constants.user.AccessLevel == 10)
                {
                    buttonRegisterUser.IsVisible = true;
                }
            } else {
                buttonLogin.IsVisible = true;
                buttonRegister.IsVisible = true;
            }
        }

        private void clearStack() {
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
        }
    }
}
