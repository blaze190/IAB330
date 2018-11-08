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
        private EquipmentDBController equipmentDBController;
        public Request ()
		{
			InitializeComponent ();
            this.equipmentDBController = new EquipmentDBController();

            

            populatePicker();
        }

        /// <summary>
        /// populate the dropdown menu
        /// </summary>
        private void populatePicker() {
            //populate the dropdown
            foreach (Equipment equip in equipmentDBController.GetAllEquipment()){
                string msg;
                if (equip.UserID == 0)
                {
                    msg = " (available)";
                }
                else {
                    msg = " (in use)";
                }
                pickerEquipment.Items.Add(equip.Name + msg);
            }
        }

        /// <summary>
        /// when the button is clicked, update the equipment in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonSubmitClicked(object sender, EventArgs e)
        {
            string pickerValue = pickerEquipment.Items[pickerEquipment.SelectedIndex];

            if (pickerValue.Contains("available")) {
                pickerValue = pickerValue.Replace(" (available)", "");
            } else if (pickerValue.Contains("in use"))
            {
                pickerValue = pickerValue.Replace(" (in use)", "");
            }

            Equipment equipment = equipmentDBController.GetEquipment(pickerValue).FirstOrDefault();

            equipment.UserID = Constants.user.Id;

            equipmentDBController.SaveEquipment(equipment);

            DisplayAlert("Success","You have successfully booked equipment","OK");
            Navigation.PushAsync(new Views.Request());

        }
    }
}