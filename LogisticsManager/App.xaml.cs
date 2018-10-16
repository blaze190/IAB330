using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LogisticsManager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            List<string> script = new List<string>();
            script.Add("Empty bins");
            script.Add("Dump in large skip outside the front gate");
            Task takeOutTrash = new Task("Take Out Trash", script);

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
