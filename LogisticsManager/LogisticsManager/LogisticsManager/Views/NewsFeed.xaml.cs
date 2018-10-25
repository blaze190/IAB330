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
	public partial class NewsFeed : ContentPage
	{
        ReportsDBController reportsDBController;
        UsersDBController usersDBController;
        public NewsFeed ()
		{
			InitializeComponent ();

            this.reportsDBController = new ReportsDBController();
            this.usersDBController = new UsersDBController();

            generateFeed();
		}

        private void generateFeed() {

            foreach (LogisticsManager.Report report in reportsDBController.GetOrderedReports()) {

                if (report.CompanyID == Constants.company.Id)
                {

                    string FromUser = usersDBController.GetUserByID(report.FromUserID).FirstOrDefault().Username;

                    Label labelDateTimeUser = new Label();
                    Label labelType = new Label();
                    Label labelDesc = new Label();

                    labelType.FontSize = 15;
                    labelDesc.FontSize = 10;
                    labelDateTimeUser.FontSize = 8;

                    labelDesc.Margin = new Thickness(0, 0, 0, 10);

                    labelType.Text = report.Type;
                    labelDesc.Text = report.Desc;
                    labelDateTimeUser.Text = FromUser + " " + report.CreationDate.ToString();

                    NewsFeedStack.Children.Add(labelDateTimeUser);
                    NewsFeedStack.Children.Add(labelType);
                    NewsFeedStack.Children.Add(labelDesc);

                }
            }
            
        }
	}
}