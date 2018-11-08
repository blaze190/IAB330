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
	public partial class NewTask : ContentPage
	{
        TasksDBController tasksDBController;
		public NewTask ()
		{
			InitializeComponent ();

            this.tasksDBController = new TasksDBController();

            //populate picker
            pickerAssignment.Items.Add("Admin");
            pickerAssignment.Items.Add("Staff");
            pickerAssignment.Items.Add("Manager");
        }

        /// <summary>
        /// Attempt to create a task when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonSubmitClicked(object sender, EventArgs e)
        {
            if (createNewTask())
            {
                DisplayAlert("Success", "You have successfully created a new task", "OK");
                Navigation.PushAsync(new MainPage());
            }
            else
                DisplayAlert("Alert","You have incorrectly filled out a field", "OK");

            
        }

        /// <summary>
        /// Create a new task and save it to the database
        /// </summary>
        /// <returns></returns>
        private bool createNewTask() {

            try
            {
                //Assign picker value an integer for storage
                string pickerValue = pickerAssignment.Items[pickerAssignment.SelectedIndex];
                int pickerInt;

                //The reason Staff is 1, Manager is 2 and Admin is 10
                //is because it leaves 7 more Access Levels to add in later.
                //Admin should always be the highest out of all access levels
                //so it's 10
                switch (pickerValue)
                {
                    case "Staff":
                        pickerInt = 1;
                        break;
                    case "Manager":
                        pickerInt = 2;
                        break;
                    case "Admin":
                        pickerInt = 10;
                        break;
                    default:
                        pickerInt = 0;
                        break;
                }

                //create new task and assign attributes
                Task task = new Task();
                task.AccessLevel = pickerInt;
                task.CompanyID = Constants.company.Id;
                task.Desc = entryDescription.Text;
                task.Name = entryName.Text;

                //save to database
                tasksDBController.SaveTask(task);

                return true;

            }
            catch (Exception) {
                return false;
            }

        }
    }
}