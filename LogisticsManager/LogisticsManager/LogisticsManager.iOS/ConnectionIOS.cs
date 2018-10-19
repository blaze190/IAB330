using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.IO;
using SQLite;
using LogisticsManager.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(ConnectionIOS))]
namespace LogisticsManager.iOS
{
    public class ConnectionIOS
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "LogManDB.db3";
            string personalFolder =
              System.Environment.
              GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder =
              Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}