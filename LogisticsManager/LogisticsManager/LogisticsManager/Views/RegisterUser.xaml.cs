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
	public partial class RegisterUser : ContentPage
	{
        private UsersDBController usersDBController;
        private CompaniesDBController companiesDBController;
        public RegisterUser()
        {
            InitializeComponent();
            this.usersDBController = new UsersDBController();
            this.companiesDBController = new CompaniesDBController();

            pickerAccessLevel.Items.Add("Staff");
            pickerAccessLevel.Items.Add("Manager");
            pickerAccessLevel.Items.Add("Admin");

            entryPassword.IsPassword = true;
        }

        private void buttonSubmitClicked(object sender, EventArgs e)
        {
            register();
        }

        private void register()
        {
            User user = new User();
            user.Username = entryUsername.Text;
            user.Password = entryPassword.Text;

            string pickerValue = pickerAccessLevel.Items[pickerAccessLevel.SelectedIndex];
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
            else {
                pickerInt = 0;
            }
            
            user.AccessLevel = pickerInt;
           
            user.CompanyID = Constants.company.Id;
            usersDBController.SaveUser(user);

            DisplayAlert("Success", "You have successfully created an account", "OK");
            Navigation.PushAsync(new MainPage());
        }
    }
}