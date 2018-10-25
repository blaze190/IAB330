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
	public partial class Register : ContentPage
	{
        private UsersDBController usersDBController;
        private CompaniesDBController companiesDBController;
        public Register ()
		{
			InitializeComponent ();
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
            Company company = new Company();
            company.Name = entryCompanyName.Text;
            companiesDBController.SaveCompany(company);

            User user = new User();
            user.Username = entryUsername.Text;
            user.Password = entryPassword.Text;
            user.AccessLevel = 10;
            user.CompanyID = company.Id;
            usersDBController.SaveUser(user);

            DisplayAlert("Success", "You have successfully created a company account", "OK");
            Navigation.PushAsync(new MainPage());
        }
    }
}