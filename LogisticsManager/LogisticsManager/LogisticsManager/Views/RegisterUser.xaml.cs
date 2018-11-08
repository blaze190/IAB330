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
            if (Valid())
            {
                DisplayAlert("Success", "You have successfully created an account", "OK");
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Alert", "You incorrectly filled out a field", "OK");
            }
            
        }

        private bool Valid()
        {
            try
            {
                User user = new User();

                //Set Username
                user.Username = entryUsername.Text;

                //Set Access Level
                string pickerValue = pickerAccessLevel.Items[pickerAccessLevel.SelectedIndex];
                int pickerInt;

                //The reason Staff is 1, Manager is 2 and Admin is 10
                //is because it leaves 7 more Access Levels to add in later.
                //Admin should always be the highest out of all access levels
                //so it's 10
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
                user.AccessLevel = pickerInt;

                //Set Company ID
                user.CompanyID = Constants.company.Id;

                //Set password

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

                //Add user in database
                usersDBController.SaveUser(user);
               
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
    }
}