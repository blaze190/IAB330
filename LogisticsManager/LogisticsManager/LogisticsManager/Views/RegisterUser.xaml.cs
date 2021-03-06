﻿using System;
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
	public partial class RegisterUser : ContentPage
	{
        private UsersDBController usersDBController;
        private CompaniesDBController companiesDBController;
        public RegisterUser()
        {
            InitializeComponent();
            this.usersDBController = new UsersDBController();
            this.companiesDBController = new CompaniesDBController();

            //populate picker
            pickerAccessLevel.Items.Add("Staff");
            pickerAccessLevel.Items.Add("Manager");
            pickerAccessLevel.Items.Add("Admin");

            entryPassword.IsPassword = true;
        }

        /// <summary>
        /// if valid, create an account when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// register a user using the specified data and save it into the database
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            try
            {
                User user = new User();

                //Set Username
                //validate username
                if (Regex.IsMatch(entryUsername.Text.ToLower(), @"[A-Za-z1-9_]+")) //matches letters, numbers and underscores
                    user.Username = entryUsername.Text.ToLower();
                else return false;

                //Set Access Level
                string pickerValue = pickerAccessLevel.Items[pickerAccessLevel.SelectedIndex];
                int pickerInt;

                //The reason Staff is 1, Manager is 2 and Admin is 10
                //is because it leaves 7 more Access Levels to add in later.
                //Admin should always be the highest out of all access levels
                //so it's 10
                switch (pickerValue)
                {
                    case "Staff":
                        pickerInt = 1;
                        break;
                    case "Manager":
                        pickerInt = 2;
                        break;
                    case "Admin":
                        pickerInt = 10;
                        break;
                    default:
                        pickerInt = 0;
                        break;
                }
                user.AccessLevel = pickerInt;

                //Set Company ID
                user.CompanyID = Constants.company.Id;

                //Set password

                //validate password
                if (Regex.IsMatch(entryPassword.Text, @"[A-Za-z0-9_!@#$%^&*()]+")) //matches letters, numbers and symbols
                    user.Password = entryPassword.Text;
                else return false;

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