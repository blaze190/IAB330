using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

            invisibleElements();
            animateElements();

            entryPassword.IsPassword = true;
        }

        private void buttonSubmitClicked(object sender, EventArgs e)
        {
            register();
        }

        private void register()
        {

            if (Valid())
            {
                Navigation.PushAsync(new Views.LogIn());
            }
            else {
                DisplayAlert("Alert", "You incorrectly filled out a field", "OK");
            }

        }

        private bool Valid() {
            try
            {
                Company company = new Company();
                company.Name = entryCompanyName.Text;
                companiesDBController.SaveCompany(company);

                User user = new User();
                user.Username = entryUsername.Text;
                user.AccessLevel = 10;
                user.CompanyID = company.Id;

                //generate salt
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                //hashing
                var pbkdf2 = new Rfc2898DeriveBytes(entryPassword.Text, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);

                //combine salt and password
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                string savedPasswordHash = Convert.ToBase64String(hashBytes);

                user.Password = savedPasswordHash;
                               
                usersDBController.SaveUser(user);

                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        void invisibleElements()
        {
            entryCompanyName.Opacity = 0;
            entryPassword.Opacity = 0;
            entryUsername.Opacity = 0;
            entryEmail.Opacity = 0;
            entryReEnterPassword.Opacity = 0;
            buttonSubmit.Opacity = 0;
        }

        async void animateElements()
        {
            await entryCompanyName.FadeTo(1, 500);
            await entryEmail.FadeTo(1, 500);
            await entryUsername.FadeTo(1, 500);
            await entryPassword.FadeTo(1, 500);
            await entryReEnterPassword.FadeTo(1, 500);
            await buttonSubmit.FadeTo(1, 500);
        }
    }
}