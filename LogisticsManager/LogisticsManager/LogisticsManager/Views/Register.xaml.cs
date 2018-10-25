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
        public Register ()
		{
			InitializeComponent ();
            this.usersDBController = new UsersDBController();

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
            user.AccessLevel = 1;
            usersDBController.SaveUser(user);

            DisplayAlert("Success", "You have successfully created an account", "OK");
            Navigation.PushAsync(new MainPage());
        }
    }
}