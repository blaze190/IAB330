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
        private ReportsDBController reportsDBController;
        public Report()
        {
            InitializeComponent();

            this.reportsDBController = new ReportsDBController();

            //populate picker
            pickerCategory.Items.Add("Death");
            pickerCategory.Items.Add("Illness");
            pickerCategory.Items.Add("Injury");
            pickerCategory.Items.Add("Safety Risk");
            pickerCategory.Items.Add("Unethical Behaviour");
            pickerCategory.Items.Add("Other");
        }

        /// <summary>
        /// lodge a report when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonSubmitClicked(object sender, EventArgs e)
        {
            if (lodgeReport()) {
                DisplayAlert("Success", "Report sent to admin", "OK");
                Navigation.PushAsync(new MainPage());
            } else 
                DisplayAlert("Alert", "You have incorrectly filled out a field", "OK");


        }

        /// <summary>
        /// lodge a report and save it into the database
        /// </summary>
        /// <returns></returns>
        bool lodgeReport()
        {
            try
            {
                string pickerValue = pickerCategory.Items[pickerCategory.SelectedIndex];

                //create report set attributes
                LogisticsManager.Report report = new LogisticsManager.Report();
                report.Type = pickerValue;
                report.Desc = entryDescription.Text;
                report.CompanyID = Constants.company.Id;
                report.FromUserID = Constants.user.Id;
                report.CreationDate = DateTime.Now;

                //save report to database
                reportsDBController.SaveReport(report);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}