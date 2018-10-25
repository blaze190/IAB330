using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LogisticsManager;

namespace LogisticsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogIn : ContentPage
	{
        private UsersDBController usersDBController;
        private CompaniesDBController companiesDBController;
        public LogIn ()
		{
			InitializeComponent ();
            

            this.usersDBController = new UsersDBController();
            this.companiesDBController = new CompaniesDBController();

            entryPassword.IsPassword = true;


        }

        void buttonSubmitClicked(object sender, EventArgs e)
        {
            if (checkPassword())
            {
                DisplayAlert("Success", "You have successfully logged in", "OK");
                Navigation.PushAsync(new MainPage());
            }
            else {
                DisplayAlert("Alert", "You entered the username or password incorrectly", "OK");
            }
        }

        bool checkPassword() {
            string qUsername = entryUsername.Text;
            string qPassword = entryPassword.Text;

            User user = usersDBController.GetUser(qUsername).FirstOrDefault();

            try {
                if (user.Password == qPassword)
                {
                    Constants.user = user;
                    Constants.company = companiesDBController.GetCompany(user.CompanyID).FirstOrDefault();
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception e) {
                return false;
            }
            
        }
    }
}