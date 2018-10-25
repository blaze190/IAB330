using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;

namespace LogisticsManager
{
    public class User : INotifyPropertyChanged
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

        private string _username;
        [NotNull, Unique]
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                this._username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        [NotNull]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                this._password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private int _accessLevel;
        [NotNull]
        public int AccessLevel
        {
            get
            {
                return _accessLevel;
            }
            set
            {
                this._accessLevel = value;
                OnPropertyChanged(nameof(AccessLevel));
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
