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

        private void Users_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Tasks());
        }

        private void NewsFeed_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.NewsFeed());
        }

        private void ClockOut_Button_Clicked(object sender, EventArgs e)
        {
            clock.UserID = Constants.user.Id;
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
            Navigation.PushAsync(new Views.LogIn());
        }

        private void hideButtons() {
            buttonTasks.IsVisible = false;
            buttonNewsFeed.IsVisible = false;
            labelClock.IsVisible = false;
            buttonClockIn.IsVisible = false;
            buttonClockOut.IsVisible = false;
            buttonUsers.IsVisible = false;
            buttonLogOut.IsVisible = false;
        }

        private void unhideButtons()
        {
            buttonTasks.IsVisible = true;
            labelClock.IsVisible = true;
            buttonLogOut.IsVisible = true;
            buttonNewsFeed.IsVisible = true;
            if (Constants.user.AccessLevel == 10)
            {
                buttonUsers.IsVisible = true;
            }
            if (Constants.clock == null)
            {
                buttonClockIn.IsVisible = true;
            }
            else {
                buttonClockOut.IsVisible = true;
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
