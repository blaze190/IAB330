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
	public partial class Request : ContentPage
	{
		public Request ()
		{
			InitializeComponent ();

            pickerEquipment.Items.Add("Ladder");
            pickerEquipment.Items.Add("Vacuum");
            pickerEquipment.Items.Add("Other");
        }
	}
}