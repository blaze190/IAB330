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
            if( Int32.TryParse(entryAccessLevel.Text, out int i) ) {
                user.AccessLevel = i;
            }
           
            user.CompanyID = Constants.company.Id;
            usersDBController.SaveUser(user);

            DisplayAlert("Success", "You have successfully created an account", "OK");
            Navigation.PushAsync(new MainPage());
        }
    }
}