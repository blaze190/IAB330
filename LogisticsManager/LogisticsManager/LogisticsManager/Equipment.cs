using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;

namespace LogisticsManager
{
    public class Equipment : INotifyPropertyChanged
    {

        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _name;
        [NotNull]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _userID;
        [NotNull]
        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                this._userID = value;
                OnPropertyChanged(nameof(UserID));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }

    }
}
