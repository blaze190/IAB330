using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LogisticsManager;
using System.Security.Cryptography;

namespace LogisticsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogIn : ContentPage
	{
        private UsersDBController usersDBController;
        private ClocksDBController clocksDBController;
        private ReportsDBController reportsDBController;
        private TasksDBController tasksDBController;
        private CompaniesDBController companiesDBController;

        public LogIn ()
		{
			InitializeComponent ();

            this.usersDBController = new UsersDBController();
            this.tasksDBController = new TasksDBController();
            this.clocksDBController = new ClocksDBController();
            this.reportsDBController = new ReportsDBController();
            this.companiesDBController = new CompaniesDBController();

            InvisibleElements();
            AnimateElements();

        }

        /// <summary>
        /// Logs user in and navigates user to the Main Page if the entered
        /// username and password are correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonSubmitClicked(object sender, EventArgs e)
        {
            if (Valid())
                Navigation.PushAsync(new MainPage());
            else 
                DisplayAlert("Alert", "You entered the username or password incorrectly", "OK");
            
        }

        void buttonRegisterClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Register());
        }

        /// <summary>
        /// Validates the password, returns false if the entered password is wrong
        /// and returns true if the entered password is correct
        /// </summary>
        /// <returns></returns>
        bool Valid() {

            try {
                User user = usersDBController.GetUser(entryUsername.Text.ToLower()).FirstOrDefault();

                //Get hashed password from database
                string savedPasswordHash = user.Password;
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

                //Generate salt
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);

                //Hash entered password
                var pbkdf2 = new Rfc2898DeriveBytes(entryPassword.Text, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);

                //Compare the hashed password from the database to the hashed entered password
                for (int i = 0; i < 20; i++)
                {
                    //return false if it doesn't match
                    if (hashBytes[i + 16] != hash[i])
                        return false;
                }
                //when we get to this point, the passwords are matched otherwise the method would return false
                //so set the user and company variables and return true
                Constants.user = user;
                Constants.company = companiesDBController.GetCompany(user.CompanyID).FirstOrDefault();
                return true;
                
            } catch (Exception) {
                return false;
            }
            
        }

        /// <summary>
        /// Sets the opacity to all elements on the page to 0
        /// </summary>
        void InvisibleElements() {
            entryPassword.Opacity = 0;
            entryUsername.Opacity = 0;
            buttonLogIn.Opacity = 0;
            buttonRegister.Opacity = 0;
            imageManagise.Opacity = 0;
        }

        /// <summary>
        /// Fades in all elements on the page one at a time
        /// </summary>
        async void AnimateElements() {
            await imageManagise.FadeTo(1, 500);
            await entryUsername.FadeTo(1, 500);
            await entryPassword.FadeTo(1, 500);
            await buttonLogIn.FadeTo(1, 500);
            await buttonRegister.FadeTo(1, 500);
        }

        /// <summary>
        /// Clears all the data in the database. 
        /// Used only for indev troubleshooting
        /// </summary>
        void ClearDatabase() {

            clocksDBController.DeleteAllClocks();
            reportsDBController.DeleteAllReports();
            usersDBController.DeleteAllUsers();
            companiesDBController.DeleteAllCompanies();
            tasksDBController.DeleteAllTasks();
        }
    }
}