﻿using System;
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

        public IEnumerable<Task> GetAllTasks()
        {
            lock (locker)
            {
                var query = from task in database.Table<Task>()
                            select task;
                return query.AsEnumerable();
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
