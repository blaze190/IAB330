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
	public partial class Report : ContentPage
	{
		public Report ()
		{
			InitializeComponent ();

            pickerCategory.Items.Add("Injury");
            pickerCategory.Items.Add("Death");
            pickerCategory.Items.Add("Other");
        }
	}
}