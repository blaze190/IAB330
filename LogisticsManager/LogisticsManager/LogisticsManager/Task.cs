using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;

namespace LogisticsManager
{
    public class Task : INotifyPropertyChanged
    {

        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id {
            get {
                return _id;
            }
            set {
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

        private string _desc;
        [NotNull]
        public string Desc
        {
            get
            {
                return _desc;
            }
            set
            {
                this._desc = value;
                OnPropertyChanged(nameof(Desc));
            }
        }

        private int _companyID;
        public int CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                this._companyID = value;
                OnPropertyChanged(nameof(CompanyID));
            }
        }

        private int _accessLevel;
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
