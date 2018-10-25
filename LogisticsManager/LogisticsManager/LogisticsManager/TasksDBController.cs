using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace LogisticsManager
{
    public class TasksDBController
    {
        private SQLiteConnection database;
        private static object locker = new object();

        public ObservableCollection<Task> Tasks { get; set; }

        public TasksDBController()
        {
            database =
              DependencyService.Get<IConnection>().
              DbConnection();
            database.CreateTable<Task>();
            this.Tasks =
              new ObservableCollection<Task>(database.Table<Task>());
            // If the table is empty, initialize the collection
            if (!database.Table<Task>().Any())
            {
                AddNewTask();
            }
            
        }

        public void AddNewTask()
        {
            this.Tasks.
              Add(new Task
              {
                  Name = "Name...",
                  Desc = "Desc..."
              });
        }

        public IEnumerable<Task> GetFilteredTasks()
        {
            lock (locker)
            {
                return database.Query<Task>(
                  "SELECT * FROM Tasks WHERE Name = 'Do Thing'").AsEnumerable();
            }
        }

        public int SaveTask(Task taskInstance)
        {
            lock (locker)
            {
                //if an Id is not equal to 0, then the task already exists
                if (taskInstance.Id != 0)
                {
                    database.Update(taskInstance);
                    return taskInstance.Id;
                }
                else
                {
                    database.Insert(taskInstance);
                    return taskInstance.Id;
                }
            }
        }

        public void SaveAllTasks()
        {
            lock (locker)
            {
                foreach (var taskInstance in this.Tasks)
                {
                    if (taskInstance.Id != 0)
                    {
                        database.Update(taskInstance);
                    }
                    else
                    {
                        database.Insert(taskInstance);
                    }
                }
            }
        }

        public int DeleteTask(Task taskInstance)
        {
            var id = taskInstance.Id;
            if (id != 0)
            {
                lock (locker)
                {
                    database.Delete<Task>(id);
                }
            }
            this.Tasks.Remove(taskInstance);
            return id;
        }

        public void DeleteAllTasks()
        {
            lock (locker)
            {
                database.DropTable<Task>();
                database.CreateTable<Task>();
            }
            this.Tasks = null;
            this.Tasks = new ObservableCollection<Task>
              (database.Table<Task>());
        }
    }
}
