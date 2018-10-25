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

        ClocksDBController clocksDBController;
        Clock clock;
        public MainPage()
        {
            InitializeComponent();

            clocksDBController = new ClocksDBController();
            clock = new Clock();

            if (Constants.clock == null)
            {
                labelClock.Text = "Clocked Out";
            }
            else {
                labelClock.Text = "Clocked In";
            }

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

        private void ClockOut_Button_Clicked(object sender, EventArgs e)
        {
            clock.ClockOut = DateTime.Now;

            clocksDBController.SaveClock(clock);

            Constants.clock = null;

            Navigation.PushAsync(new MainPage());
        }

        private void ClockIn_Button_Clicked(object sender, EventArgs e)
        {
            clock.UserID = Constants.user.Id;
            clock.ClockIn = DateTime.Now;

            clocksDBController.SaveClock(clock);

            Constants.clock = clock;

            Navigation.PushAsync(new MainPage());
        }

        private void Logout_Button_Clicked(object sender, EventArgs e)
        {
            Constants.user = null;
            Constants.company = null;
            Constants.clock = null;
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
            buttonNewsFeed.IsVisible = false;
            labelClock.IsVisible = false;
            buttonClockIn.IsVisible = false;
            buttonClockOut.IsVisible = false;
        }

        private void unhideButtons()
        {
            if (Constants.user != null)
            {
                buttonTasks.IsVisible = true;
                buttonReport.IsVisible = true;
                buttonRequest.IsVisible = true;
                buttonLogout.IsVisible = true;
                labelClock.IsVisible = true;
                if (Constants.user.AccessLevel == 10)
                {
                    buttonRegisterUser.IsVisible = true;
                    buttonNewsFeed.IsVisible = true;
                }
                if (Constants.clock == null)
                {
                    buttonClockIn.IsVisible = true;
                }
                else {
                    buttonClockOut.IsVisible = true;
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
