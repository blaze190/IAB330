using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LogisticsManager.Droid;
using System.IO;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(ConnectionAndroid))]
namespace LogisticsManager.Droid
{
    public class ConnectionAndroid : IConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "LogManDB.db3";
            var path = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}