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
                labelClock.Text = "You are Clocked Out";
            else 
                labelClock.Text = "You are Clocked In";
            

            clearStack();
            hideButtons();
            unhideButtons();
         
        }

        private void Tasks_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Tasks());
        }

        private void Report_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Report());
        }

        private async void Users_Button_Clicked(object sender, EventArgs e)
        {
            var actionSheet = await DisplayActionSheet("Select an Option", "Cancel", null, "Register User", "Report");

            switch (actionSheet)
            {
                case "Register User":
                    await Navigation.PushAsync(new Views.RegisterUser());
                    break;

                case "Report":
                    await Navigation.PushAsync(new Views.Report());
                    break;
            }
        }

        private void NewsFeed_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.NewsFeed());
        }

        private void ClockOut_Button_Clicked(object sender, EventArgs e)
        {
            clock.UserID = Constants.user.Id;
            clock.ClockOut = DateTime.Now;
            clock.ClockIn = DateTime.Parse("1/1/0001");

            clocksDBController.SaveClock(clock);

            Constants.clock = null;

            Navigation.PushAsync(new MainPage());
        }

        private void ClockIn_Button_Clicked(object sender, EventArgs e)
        {
            clock.UserID = Constants.user.Id;
            clock.ClockIn = DateTime.Now;
            clock.ClockOut = DateTime.Parse("1/1/0001");

            clocksDBController.SaveClock(clock);

            Constants.clock = clock;

            Navigation.PushAsync(new MainPage());
        }

        private void Logout_Button_Clicked(object sender, EventArgs e)
        {
            if (Constants.clock == null)
            {
                Constants.user = null;
                Constants.company = null;
                Constants.clock = null;
                Navigation.PushAsync(new Views.LogIn());
            }
            else
                DisplayAlert("Alert","You must clock out before you log off","OK");
            
        }

        private void hideButtons() {
            buttonTasks.IsVisible = false;
            buttonNewsFeed.IsVisible = false;
            labelClock.IsVisible = false;
            buttonClockIn.IsVisible = false;
            buttonClockOut.IsVisible = false;
            buttonUsers.IsVisible = false;
            buttonLogOut.IsVisible = false;
            buttonReport.IsVisible = false;
        }

        private void unhideButtons()
        {
            buttonTasks.IsVisible = true;
            labelClock.IsVisible = true;
            buttonLogOut.IsVisible = true;
            buttonNewsFeed.IsVisible = true;

            if (Constants.user.AccessLevel == 10)
                buttonUsers.IsVisible = true;
            else
                buttonReport.IsVisible = true;
            
            if (Constants.clock == null)
                buttonClockIn.IsVisible = true;
            else 
                buttonClockOut.IsVisible = true;
            
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
