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

            generateList();

        }

        void OnAdd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.NewTask());
        }

        void OnDeleteAll(object sender, EventArgs e)
        {
            tasksDBController.DeleteAllTasks();
            DisplayAlert("Success","All tasks have been removed","OK");
            Navigation.PushAsync(new MainPage());
        }

        private void generateList() {

            foreach (Task task in tasksDBController.GetAllTasks())
            {
                if (task.CompanyID == Constants.company.Id) {

                    if (task.AccessLevel == Constants.user.AccessLevel)
                    {

                        Label labelName = new Label();
                        Label labelDesc = new Label();
                        Button buttonCompleted = new Button();

                        labelName.FontSize = 15;
                        labelDesc.FontSize = 10;
                        buttonCompleted.FontSize = 13;

                        buttonCompleted.Margin = new Thickness(0, 0, 0, 10);

                        labelName.Text = task.Name;
                        labelDesc.Text = task.Desc;
                        buttonCompleted.Text = "Complete";

                        buttonCompleted.Clicked += HandleClick;

                        void HandleClick(object sender, EventArgs e) {
                            tasksDBController.DeleteTask(task);
                            Navigation.PushAsync(new MainPage());
                        }

                        TasksStack.Children.Add(labelName);
                        TasksStack.Children.Add(labelDesc);
                        TasksStack.Children.Add(buttonCompleted);

                    }
                }
            }
        }
    }
}