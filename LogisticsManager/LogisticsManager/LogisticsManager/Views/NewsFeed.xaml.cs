using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogisticsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsFeed : ContentPage
	{
        ReportsDBController reportsDBController;
        UsersDBController usersDBController;
        ClocksDBController clocksDBController;
        List<Entry> entries;

        public NewsFeed ()
		{
			InitializeComponent ();

            this.reportsDBController = new ReportsDBController();
            this.usersDBController = new UsersDBController();
            this.clocksDBController = new ClocksDBController();
            entries = new List<Entry>();

            generateFeed();
		}

        /// <summary>
        /// Returns a string to be used in Font Awesome depending on the parameter
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string getIcon(string type) {

            switch (type) {

                case "Death":
                    return "Skull-crossbones";

                case "Illness":
                    return "Stethoscope";

                case "Injury":
                    return "User-injured";

                case "Unethical Behaviour":
                    return "Angry";

                case "Clock":
                    return "User-clock";

                default:
                    return "Exclamation-triangle";

            }

        }

        /// <summary>
        /// generates a grid for each entry in the newsfeed and adds it to the stack layout
        /// </summary>
        private void generateFeed() {

            //Add reports to the entry list if the user is admin
            if (Constants.user.AccessLevel == 10)
            {
                foreach (LogisticsManager.Report report in reportsDBController.GetOrderedReports())
                {
                    if (report.CompanyID == Constants.company.Id)
                    {
                        string user = usersDBController.GetUserByID(report.FromUserID).FirstOrDefault().Username;
                        DateTime dateTime = report.CreationDate;
                        string desc = report.Desc;
                        string icon = getIcon(report.Type);

                        Entry entry = new Entry(icon, desc, dateTime, user);

                        entries.Add(entry);
                    }
                }
            }

            //Add clock ins to the entry list
            foreach (Clock clock in clocksDBController.GetOrderedClockIns())
            {
                if (usersDBController.GetUserByID(clock.UserID).FirstOrDefault() != null) {

                    if (clock.ClockOut.ToString().Contains("1/1/0001 12:00:00 AM"))
                    {
                        if (usersDBController.GetUserByID(clock.UserID).FirstOrDefault().CompanyID == Constants.company.Id)
                        {
                            string user = usersDBController.GetUserByID(clock.UserID).FirstOrDefault().Username;
                            DateTime dateTime = clock.ClockIn;
                            string desc = user + " has clocked in";
                            string icon = getIcon("Clock");

                            Entry entry = new Entry(icon, desc, dateTime, user);

                            entries.Add(entry);
                        }
                    }
                }
            }

            //Add clock outs to the entry list
            foreach (Clock clock in clocksDBController.GetOrderedClockOuts())
            {
                if (usersDBController.GetUserByID(clock.UserID).FirstOrDefault() != null)
                {

                    if (!clock.ClockOut.ToString().Contains("1/1/0001 12:00:00 AM"))
                    {
                        if (usersDBController.GetUserByID(clock.UserID).FirstOrDefault().CompanyID == Constants.company.Id)
                        {
                            string user = usersDBController.GetUserByID(clock.UserID).FirstOrDefault().Username;
                            DateTime dateTime = clock.ClockOut;
                            string desc = user + " has clocked out";
                            string icon = getIcon("Clock");

                            Entry entry = new Entry(icon, desc, dateTime, user);

                            entries.Add(entry);
                        }
                    }

                }   
            }

            //Order entries by date
            entries = entries.OrderByDescending(o => o.GetDateTime()).ToList();

            //remove oldest entries
            for (int i = 14; i < entries.Count(); i++) {
                entries.RemoveAt(i);
            }

            //Create grid for each entry
            foreach (Entry entry in entries)
            {

                //Create grid
                Grid grid = new Grid();
                grid.Margin = new Thickness(0, 10, 0, 0);
                grid.HorizontalOptions = LayoutOptions.FillAndExpand;
                grid.Padding = new Thickness(5,5,5,5);

                //Define grid columns and rows
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

                //Create elements to put in grid
                Label labelDateTimeUser = new Label();
                Label labelIcon = new Label();
                Label labelDesc = new Label();
                Button buttonRemove = new Button();
                BoxView boxView = new BoxView();

                //Customise boxView used as a seperator
                boxView.HeightRequest = 1;
                boxView.Color = Color.FromHex("#212121"); //black
                boxView.Opacity = 0.5;

                //Customise entry description
                labelDesc.FontSize = 10;
                labelDesc.TextColor = Color.FromHex("#212121"); //Black
                labelDesc.VerticalOptions = LayoutOptions.Center;

                //Customise date time and user label
                labelDateTimeUser.FontSize = 8;

                //Customise remove button
                buttonRemove.WidthRequest = 35;
                buttonRemove.HeightRequest = 35;
                buttonRemove.BackgroundColor = Color.Transparent;
                buttonRemove.TextColor = Color.FromHex("#212121"); //Black
                buttonRemove.Clicked += HandleClick;

                //Customise icon
                labelIcon.TextColor = Color.FromHex("#212121");
                labelIcon.VerticalOptions = LayoutOptions.Center;

                //Change font family of the icon and remove button to the Font Awesome Solid font
                //depending on the platform
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        labelIcon.FontFamily = "Font Awesome 5 Free";
                        buttonRemove.FontFamily = "Font Awesome 5 Free";
                        break;
                    case Device.Android:
                        labelIcon.FontFamily = "Font Awesome 5 Free-Solid-900.otf#Font Awesome 5 Free Solid";
                        buttonRemove.FontFamily = "Font Awesome 5 Free-Solid-900.otf#Font Awesome 5 Free Solid";
                        break;
                    case Device.UWP:
                        labelIcon.FontFamily = "Assets/Font Awesome 5 Free-Solid-900.otf#Font Awesome 5 Free";
                        buttonRemove.FontFamily = "Assets/Font Awesome 5 Free-Solid-900.otf#Font Awesome 5 Free";
                        break;
                }

                //Add text to elements
                labelIcon.Text = entry.GetIcon();
                labelDesc.Text = entry.GetDesc();
                labelDateTimeUser.Text = entry.GetUser() + " " + entry.GetDateTime().ToString();
                buttonRemove.Text = "Times";

                //Add elements to the grid
                grid.Children.Add(labelIcon, 0, 0);
                grid.Children.Add(labelDesc, 1, 0);
                grid.Children.Add(buttonRemove, 2, 0);
                grid.Children.Add(labelDateTimeUser, 1, 1);

                //Add grid and box view to the stack layout
                NewsFeedStack.Children.Add(grid);
                NewsFeedStack.Children.Add(boxView);

                //Remove entry when the remove button is clicked
                async void HandleClick(object sender, EventArgs e)
                {
                    //fade entry
                    await grid.FadeTo(0, 500);

                    //remove entry when fade is complete
                    NewsFeedStack.Children.Remove(grid);
                    NewsFeedStack.Children.Remove(boxView);
                }

                
            }

        }
	}
}