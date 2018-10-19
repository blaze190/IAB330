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
	public partial class LogIn : ContentPage
	{
		public LogIn ()
		{
			InitializeComponent ();

            entryUsername.Text = "";
            entryPassword.Text = "";

            BackgroundColor = Color.White;
            labelUsername.TextColor = Color.Black;
            labelPassword.TextColor = Color.Black;

        }

        void buttonSubmitClicked(object sender, EventArgs e)
        {
        }
    }
}