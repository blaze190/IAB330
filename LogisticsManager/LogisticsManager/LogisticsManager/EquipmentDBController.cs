using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace LogisticsManager
{
    public class EquipmentDBController
    {
        private SQLiteConnection database;
        private static object locker = new object();

        public ObservableCollection<Equipment> Equipments { get; set; }

        public EquipmentDBController()
        {
            database =
              DependencyService.Get<IConnection>().
              DbConnection();
            database.CreateTable<Equipment>();
            this.Equipments =
              new ObservableCollection<Equipment>(database.Table<Equipment>());
            // If the table is empty, initialize the collection
            if (!database.Table<Equipment>().Any())
            {
                AddNewEquipment("default",'0');
            }
            
        }

        public void AddNewEquipment(string name, int userID)
        {
            this.Equipments.
              Add(new Equipment
              {
                  Name = name,
                  UserID = userID,
                  CompanyID = 0
              });
        }

        public IEnumerable<Equipment> GetAllEquipment()
        {
            lock (locker)
            {
                var query = from equip in database.Table<Equipment>()
                            select equip;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<Equipment> GetEquipment(string name)
        {
            lock (locker)
            {
                var query = from equip in database.Table<Equipment>()
                            where equip.Name == name
                            select equip;
                return query.AsEnumerable();
            }
        }

        public int SaveEquipment(Equipment equipmentInstance)
        {
            lock (locker)
            {
                //if an Id is not equal to 0, then the task already exists
                if (equipmentInstance.Id != 0)
                {
                    database.Update(equipmentInstance);
                    return equipmentInstance.Id;
                }
                else
                {
                    database.Insert(equipmentInstance);
                    return equipmentInstance.Id;
                }
            }
        }

        public int DeleteEquipment(Equipment equipmentInstance)
        {
            var id = equipmentInstance.Id;
            if (id != 0)
            {
                lock (locker)
                {
                    database.Delete<Equipment>(id);
                }
            }
            this.Equipments.Remove(equipmentInstance);
            return id;
        }

        public void DeleteAllEquipment()
        {
            lock (locker)
            {
                database.DropTable<Equipment>();
                database.CreateTable<Equipment>();
            }
            this.Equipments = null;
            this.Equipments = new ObservableCollection<Equipment>
              (database.Table<Equipment>());
        }
    }
}
