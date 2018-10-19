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
        public Tasks()
		{
			InitializeComponent();
            this.tasksDBController = new TasksDBController();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = this.tasksDBController;
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            this.tasksDBController.SaveAllTasks();
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            this.tasksDBController.AddNewTask();
        }

        private void OnRemoveClick(object sender, EventArgs e)
        {
            var currentTask =
              this.TasksView.SelectedItem as Task;
            if (currentTask != null)
            {
                this.tasksDBController.DeleteTask(currentTask);
            }
        }

        private async void OnRemoveAllClick(object sender, EventArgs e)
        {
            if (this.tasksDBController.Tasks.Any())
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure? This cannot be undone",
                  "OK", "Cancel");
                if (result == true)
                {
                    this.tasksDBController.DeleteAllTasks();
                    this.BindingContext = this.tasksDBController;
                }
            }
        }
    }
}