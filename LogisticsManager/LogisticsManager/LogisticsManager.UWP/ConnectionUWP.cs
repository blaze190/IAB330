using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Windows.Storage;
using System.IO;
using LogisticsManager.UWP;

[assembly: Dependency(typeof(ConnectionUWP))]
namespace LogisticsManager.UWP
{
    public class ConnectionUWP : IConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "LogManDB.db3";
            var path = Path.Combine(ApplicationData.
              Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }
    }
}
