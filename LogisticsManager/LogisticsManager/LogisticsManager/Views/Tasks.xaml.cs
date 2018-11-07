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
	public partial class Tasks : ContentPage
	{
        private TasksDBController tasksDBController;
        List<Task> tasks;

        public Tasks()
		{
			InitializeComponent();
            this.tasksDBController = new TasksDBController();
            tasks = new List<Task>();

            if (Constants.user.AccessLevel != 10)
                ToolbarItems.RemoveAt(0);

            generateList();

        }

        void AddClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.NewTask());
        }

        string GetAccessLevel(int accessLevelInt) {

            switch (accessLevelInt) {
                case 1:
                    return "Staff";
                case 2:
                    return "Managers";
                case 10:
                    return "Administrators";
                default:
                    return "Unknown Access Level";

            }

        }

        private void generateList() {

            //Add tasks to the task list if the access level and company matches
            foreach (Task task in tasksDBController.GetAllTasks())
            {
                if (task.CompanyID == Constants.company.Id && task.AccessLevel == Constants.user.AccessLevel)
                    tasks.Add(task);
                
            }

            //Create grid for each task
            foreach (Task task in tasks)
            {

                //Create grid
                Grid grid = new Grid();
                grid.Margin = new Thickness(0, 10, 0, 0);
                grid.HorizontalOptions = LayoutOptions.FillAndExpand;
                grid.Padding = new Thickness(5, 5, 5, 5);

                //Define grid columns and rows
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

                //Create elements to put in grid
                Label labelName = new Label();
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
                labelName.FontSize = 8;

                //Customise remove button
                buttonRemove.WidthRequest = 40;
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
                labelIcon.Text = "Thumbtack";
                labelDesc.Text = task.Desc;
                labelName.Text = task.Name + " assigned to all " + GetAccessLevel(task.AccessLevel);
                buttonRemove.Text = "Check";

                //Add elements to the grid
                grid.Children.Add(labelIcon, 0, 1);
                grid.Children.Add(labelDesc, 1, 1);
                grid.Children.Add(buttonRemove, 2, 1);
                grid.Children.Add(labelName, 1, 0);

                //Add grid and box view to the stack layout
                TasksStack.Children.Add(grid);
                TasksStack.Children.Add(boxView);

                //Remove entry when the remove button is clicked
                async void HandleClick(object sender, EventArgs e)
                {
                    //fade entry
                    await grid.FadeTo(0, 500);

                    //remove entry when fade is complete
                    TasksStack.Children.Remove(grid);
                    TasksStack.Children.Remove(boxView);

                    //delete task from database
                    tasksDBController.DeleteTask(task);
                }


            }          
            
        }
    }
}