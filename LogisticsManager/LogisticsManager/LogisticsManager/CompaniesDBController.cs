using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace LogisticsManager
{
    public class CompaniesDBController
    {
        private SQLiteConnection database;
        private static object locker = new object();

        public ObservableCollection<Company> Companies { get; set; }

        public CompaniesDBController()
        {
            database =
              DependencyService.Get<IConnection>().
              DbConnection();
            database.CreateTable<Company>();
            this.Companies =
              new ObservableCollection<Company>(database.Table<Company>());
            // If the table is empty, initialize the collection
            if (!database.Table<Company>().Any())
            {
                AddNewCompany();
            }

        }

        public void AddNewCompany()
        {
            this.Companies.
              Add(new Company
              {
                  Name = "Default"
              });
        }

        public IEnumerable<Company> GetCompany(int id)
        {
            lock (locker)
            {
                var query = from comp in database.Table<Company>()
                            where comp.Id == id
                            select comp;
                return query.AsEnumerable();
            }
        }

        public int SaveCompany(Company companyInstance)
        {
            lock (locker)
            {
                //if an Id is not equal to 0, then the task already exists
                if (companyInstance.Id != 0)
                {
                    database.Update(companyInstance);
                    return companyInstance.Id;
                }
                else
                {
                    database.Insert(companyInstance);
                    return companyInstance.Id;
                }
            }
        }

        public int DeleteCompany(Company companyInstance)
        {
            var id = companyInstance.Id;
            if (id != 0)
            {
                lock (locker)
                {
                    database.Delete<Company>(id);
                }
            }
            this.Companies.Remove(companyInstance);
            return id;
        }

        public void DeleteAllCompanies()
        {
            lock (locker)
            {
                database.DropTable<Company>();
                database.CreateTable<Company>();
            }
            this.Companies = null;
            this.Companies = new ObservableCollection<Company>
              (database.Table<Company>());
        }
    }
}
