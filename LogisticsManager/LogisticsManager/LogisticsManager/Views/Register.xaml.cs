using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Register an account when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSubmitClicked(object sender, EventArgs e)
        {
            register();
        }

        /// <summary>
        /// Attempt to register account, if invalid a message will appear
        /// </summary>
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

        /// <summary>
        /// Register an account and save it to the database
        /// </summary>
        /// <returns></returns>
        private bool Valid() {
            try
            {
                //create a company and save to database
                Company company = new Company();

                //validate company name
                if (Regex.IsMatch(entryCompanyName.Text, @"[A-Za-z1-9 _]+")) //matches letters, numbers, spaces and underscores
                    company.Name = entryCompanyName.Text;
                else return false;

                companiesDBController.SaveCompany(company);

                //create a user
                User user = new User();

                //validate username
                if (Regex.IsMatch(entryUsername.Text.ToLower(), @"[A-Za-z1-9_]+")) //matches letters, numbers and underscores
                    user.Username = entryUsername.Text.ToLower();
                else return false;
                
                user.AccessLevel = 10;
                user.CompanyID = company.Id;

                //validate password
                if (Regex.IsMatch(entryPassword.Text, @"[A-Za-z0-9_!@#$%^&*()]+")) //matches letters, numbers and symbols
                    user.Password = entryPassword.Text;
                else return false;

                //check re-enter password matches password
                if (entryReEnterPassword.Text != entryPassword.Text)
                    return false;

                //generate salt for password
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                //hash password
                var pbkdf2 = new Rfc2898DeriveBytes(entryPassword.Text, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);

                //combine
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                string savedPasswordHash = Convert.ToBase64String(hashBytes);

                //set password
                user.Password = savedPasswordHash;
                              
                //save user to the database
                usersDBController.SaveUser(user);

                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        /// <summary>
        /// Set the opacity to all elements to 0
        /// </summary>
        void invisibleElements()
        {
            entryCompanyName.Opacity = 0;
            entryPassword.Opacity = 0;
            entryUsername.Opacity = 0;
            entryEmail.Opacity = 0;
            entryReEnterPassword.Opacity = 0;
            buttonSubmit.Opacity = 0;
        }

        /// <summary>
        /// Fade in all elemets one at a time
        /// </summary>
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