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

            pickerCategory.Items.Add("Death");
            pickerCategory.Items.Add("Illness");
            pickerCategory.Items.Add("Injury");
            pickerCategory.Items.Add("Safety Risk");
            pickerCategory.Items.Add("Unethical Behaviour");
            pickerCategory.Items.Add("Other");
        }

        void buttonSubmitClicked(object sender, EventArgs e)
        {
            lodgeReport();
        }

        void lodgeReport()
        {
            string pickerValue = pickerCategory.Items[pickerCategory.SelectedIndex];

            LogisticsManager.Report report = new LogisticsManager.Report();
            report.Type = pickerValue;
            report.Desc = entryDescription.Text;
            report.CompanyID = Constants.company.Id;
            report.FromUserID = Constants.user.Id;
            report.CreationDate = DateTime.Now;

            reportsDBController.SaveReport(report);

            DisplayAlert("Success", "Report sent to admin", "OK");
            Navigation.PushAsync(new MainPage());
        }
    }
}