using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace LogisticsManager
{
    public class ReportsDBController
    {
        private SQLiteConnection database;
        private static object locker = new object();

        public ObservableCollection<Report> Reports { get; set; }

        public ReportsDBController()
        {
            database =
              DependencyService.Get<IConnection>().
              DbConnection();
            database.CreateTable<Report>();
            this.Reports =
              new ObservableCollection<Report>(database.Table<Report>());
            // If the table is empty, initialize the collection
            if (!database.Table<Report>().Any())
            {
                AddNewReport();
            }

        }

        public void AddNewReport()
        {
            this.Reports.
              Add(new Report
              {
                  Type = "Type",
                  Desc = "Desc",
                  CompanyID = 0,
                  FromUserID = 0
              });
        }

        public IEnumerable<Report> GetOrderedReports()
        {
            lock (locker)
            {
                var query = from rep in database.Table<Report>()
                            orderby rep.CreationDate descending
                            select rep;
                return query.AsEnumerable();
            }
        }

        public int SaveReport(Report reportInstance)
        {
            lock (locker)
            {
                //if an Id is not equal to 0, then the Report already exists
                if (reportInstance.Id != 0)
                {
                    database.Update(reportInstance);
                    return reportInstance.Id;
                }
                else
                {
                    database.Insert(reportInstance);
                    return reportInstance.Id;
                }
            }
        }

        public void SaveAllReports()
        {
            lock (locker)
            {
                foreach (var reportInstance in this.Reports)
                {
                    if (reportInstance.Id != 0)
                    {
                        database.Update(reportInstance);
                    }
                    else
                    {
                        database.Insert(reportInstance);
                    }
                }
            }
        }

        public int DeleteReport(Report reportInstance)
        {
            var id = reportInstance.Id;
            if (id != 0)
            {
                lock (locker)
                {
                    database.Delete<Report>(id);
                }
            }
            this.Reports.Remove(reportInstance);
            return id;
        }

        public void DeleteAllReports()
        {
            lock (locker)
            {
                database.DropTable<Report>();
                database.CreateTable<Report>();
            }
            this.Reports = null;
            this.Reports = new ObservableCollection<Report>
              (database.Table<Report>());
        }
    }
}
