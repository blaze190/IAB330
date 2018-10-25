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

            pickerAssignment.Items.Add("Admin");
            pickerAssignment.Items.Add("Staff");
            pickerAssignment.Items.Add("Manager");
        }

        void buttonSubmitClicked(object sender, EventArgs e)
        {
            createNewTask();
        }

        private void createNewTask() {

            string pickerValue = pickerAssignment.Items[pickerAssignment.SelectedIndex];
            int pickerInt;

            if (pickerValue == "Staff")
            {
                pickerInt = 1;
            }
            else if (pickerValue == "Manager")
            {
                pickerInt = 2;
            }
            else if (pickerValue == "Admin")
            {
                pickerInt = 10;
            }
            else
            {
                pickerInt = 0;
            }

            Task task = new Task();
            task.AccessLevel = pickerInt;
            task.CompanyID = Constants.company.Id;
            task.Desc = entryDescription.Text;
            task.Name = entryName.Text;

            tasksDBController.SaveTask(task);

            DisplayAlert("Success", "You have successfully created a new task", "OK");
            Navigation.PushAsync(new MainPage());

        }
    }
}