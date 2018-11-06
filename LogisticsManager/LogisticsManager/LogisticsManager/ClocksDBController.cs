using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace LogisticsManager
{
    class ClocksDBController
    {
        private SQLiteConnection database;
        private static object locker = new object();

        public ObservableCollection<Clock> Clocks { get; set; }

        public ClocksDBController()
        {
            database =
              DependencyService.Get<IConnection>().
              DbConnection();
            database.CreateTable<Clock>();
            this.Clocks =
              new ObservableCollection<Clock>(database.Table<Clock>());
            // If the table is empty, initialize the collection
            if (!database.Table<Clock>().Any())
            {
                AddNewClock();
            }

        }

        public void AddNewClock()
        {
            this.Clocks.
              Add(new Clock
              {
                  UserID = 0
              });
        }

        public IEnumerable<Clock> GetClock(int id)
        {
            lock (locker)
            {
                var query = from clock in database.Table<Clock>()
                            where clock.Id == id
                            select clock;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<Clock> GetOrderedClockIns()
        {
            lock (locker)
            {
                var query = from clock in database.Table<Clock>()
                            orderby clock.ClockIn descending
                            select clock;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<Clock> GetOrderedClockOuts()
        {
            lock (locker)
            {
                var query = from clock in database.Table<Clock>()
                            orderby clock.ClockOut descending
                            select clock;
                return query.AsEnumerable();
            }
        }

        public int SaveClock(Clock clockInstance)
        {
            lock (locker)
            {
                //if an Id is not equal to 0, then the task already exists
                if (clockInstance.Id != 0)
                {
                    database.Update(clockInstance);
                    return clockInstance.Id;
                }
                else
                {
                    database.Insert(clockInstance);
                    return clockInstance.Id;
                }
            }
        }

        public void SaveAllClocks()
        {
            lock (locker)
            {
                foreach (var clockInstance in this.Clocks)
                {
                    if (clockInstance.Id != 0)
                    {
                        database.Update(clockInstance);
                    }
                    else
                    {
                        database.Insert(clockInstance);
                    }
                }
            }
        }

        public int DeleteClock(Clock clockInstance)
        {
            var id = clockInstance.Id;
            if (id != 0)
            {
                lock (locker)
                {
                    database.Delete<Clock>(id);
                }
            }
            this.Clocks.Remove(clockInstance);
            return id;
        }

        public void DeleteAllClocks()
        {
            lock (locker)
            {
                database.DropTable<Clock>();
                database.CreateTable<Clock>();
            }
            this.Clocks = null;
            this.Clocks = new ObservableCollection<Clock>
              (database.Table<Clock>());
        }
    }
}
