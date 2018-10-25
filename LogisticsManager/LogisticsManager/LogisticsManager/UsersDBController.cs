using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace LogisticsManager
{
    public class UsersDBController
    {
        private SQLiteConnection database;
        private static object locker = new object();

        public ObservableCollection<User> Users { get; set; }

        public UsersDBController()
        {
            database =
              DependencyService.Get<IConnection>().
              DbConnection();
            database.CreateTable<User>();
            this.Users =
              new ObservableCollection<User>(database.Table<User>());
            // If the table is empty, initialize the collection
            if (!database.Table<User>().Any())
            {
                AddNewUser();
            }

        }

        public void AddNewUser()
        {
            this.Users.
              Add(new User
              {
                  Username = "Name...",
                  Password = "Desc...",
                  AccessLevel = 1
              });
        }

        public IEnumerable<User> GetUser(string username)
        {
            lock (locker)
            {
                var query = from user in database.Table<User>()
                            where user.Username == username
                            select user;
                return query.AsEnumerable();
            }
        }

        public int SaveUser(User userInstance)
        {
            lock (locker)
            {
                //if an Id is not equal to 0, then the user already exists
                if (userInstance.Id != 0)
                {
                    database.Update(userInstance);
                    return userInstance.Id;
                }
                else
                {
                    database.Insert(userInstance);
                    return userInstance.Id;
                }
            }
        }

        public void SaveAllUsers()
        {
            lock (locker)
            {
                foreach (var userInstance in this.Users)
                {
                    if (userInstance.Id != 0)
                    {
                        database.Update(userInstance);
                    }
                    else
                    {
                        database.Insert(userInstance);
                    }
                }
            }
        }

        public int DeleteUser(User userInstance)
        {
            var id = userInstance.Id;
            if (id != 0)
            {
                lock (locker)
                {
                    database.Delete<User>(id);
                }
            }
            this.Users.Remove(userInstance);
            return id;
        }

        public void DeleteAllUsers()
        {
            lock (locker)
            {
                database.DropTable<User>();
                database.CreateTable<User>();
            }
            this.Users = null;
            this.Users = new ObservableCollection<User>
              (database.Table<User>());
        }
    }
}
